using Android.Content;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LitleW.Droid.Scaner
{
    public class BarcodeScannerManager: Java.Lang.Object, EMDKManager.IEMDKListener, IBarcodeScannerManager
    {
        // EMDK variables
        private EMDKManager _emdkManager;
        private BarcodeManager _barcodeManager;
        private Scanner _scanner;

        // IBarcodeScannerManager Properties
        public event EventHandler<Scanner.DataEventArgs> ScanReceived;
        public bool IsScannerEnabled { get; private set; }

        public BarcodeScannerManager()
        {
            //Java.Lang.ClassNotFoundException: Didn't find class "com.symbol.emdk.EMDKManager$EMDKListener" on path: DexPathList[[zip file "/data/app/deskit.ru.litlew-K7o0pqO5XQcsDts3HpoZRw==/base.apk"],nativeLibraryDirectories=[/data/app/deskit.ru.litlew-K7o0pqO5XQcsDts3H…}
            //Java.Lang.ClassNotFoundException: Didn't find class "com.symbol.emdk.EMDKManager$EMDKListener" on path: DexPathList[[zip file "/data/app/deskit.ru.litlew-e4pSmy1uP-GyRi_3o_lD6w==/base.apk"],nativeLibraryDirectories=[/data/app/deskit.ru.litlew-e4pSmy1uP-GyRi_3o…}
        }

        public BarcodeScannerManager(Context context)
        {
            InitializeEMDK(context);
        }

        void InitializeEMDK(Context context)
        {
            // To retrieve the EMDKManager correctly this code requires
            // an instance of EMDKMAnager.IEMDKListener which is why 
            // we can pass the current instance via 'this'
            EMDKResults results = EMDKManager.GetEMDKManager(context, this);
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                // If there is a problem initializing throw an exception
                throw new InvalidOperationException("Unable to initialize EMDK Manager");
            }
        }

        void InitializeBarcodeManager()
        {
            _barcodeManager = (BarcodeManager)_emdkManager?.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
            if (_barcodeManager == null)
                return;

            if (_barcodeManager.SupportedDevicesInfo?.Count > 0)
                _scanner = _barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);
        }

        public void OnClosed()
        {
            if (_scanner != null)
            {
                // Ensure the scanner is not active
                _scanner.Disable();
                _scanner.Release();
                _scanner = null;
            }

            if (_emdkManager != null)
            {
                // Not that this is calling 'Release' instead of 'Dispose'
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

        public void OnOpened(EMDKManager manager)
        {
            _emdkManager = manager;

            // We can only initialize the barcode manager once we retrieve
            // the EMDK Manager, which is invoked by via callback in the
            // EMDK library
            InitializeBarcodeManager();
        }

        public void Enable()
        {
            // Just in case the manager isn't initialized, check for null
            if (_scanner == null)
                return;

            // Wire-up EMDK scanner events
            _scanner.Data += OnScanReceived;
            _scanner.Status += OnStatusChanged;

            // Enable the scanner
            _scanner.Enable();

            // Define the scanner to trigger by the hardware scanner button
            _scanner.TriggerType = Scanner.TriggerTypes.Hard;

            // Update the state of the scanner property
            IsScannerEnabled = true;
        }

        public void Disable()
        {
            if (_scanner == null)
                return;

            // Deallocate event listeners to prevent memory leaks
            _scanner.Data -= OnScanReceived;
            _scanner.Status -= OnStatusChanged;

            // Notify the EMDK Library to turn the barcode scanner off
            _scanner.Disable();

            // Reset the status of the manager
            IsScannerEnabled = false;
        }

        void OnScanReceived(object sender, Scanner.DataEventArgs args)
        {
            // Pass the EMDK events to the our new event handler
            ScanReceived?.Invoke(sender, args);
        }

        void OnStatusChanged(object sender, Scanner.StatusEventArgs args)
        {
            if (args?.P0?.State == StatusData.ScannerStates.Idle)
            {
                // Wait for 100ms before starting the next scan listen
                Task.Delay(100);

                // When the device idle's it is time to listen for a new scan
                _scanner.Read();
            }
        }
    }
}
