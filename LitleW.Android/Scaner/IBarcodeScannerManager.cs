using System;
using Symbol.XamarinEMDK.Barcode;
namespace LitleW.Droid.Scaner
{
    public interface IBarcodeScannerManager: IDisposable
    {
        event EventHandler<Scanner.DataEventArgs> ScanReceived;
        bool IsScannerEnabled { get; }
        void Enable();
        void Disable();
    }
}
