using System;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Interfaces;

namespace SconitTesting.Utility
{
    public class UseDialogOnce : IDisposable
    {
        private DialogWatcher _watcher;
        private IDialogHandler _handler;

        public UseDialogOnce( DialogWatcher watcher, IDialogHandler handler )
        {
            if ( watcher == null )
                throw new ArgumentNullException( "watcher" );
            if ( handler == null )
                throw new ArgumentNullException( "handler" );

            _watcher = watcher;
            _handler = handler;

            watcher.Add( handler );
        }


        #region IDisposable Members

        private bool disposed = false;
        public void Dispose()
        {
            Dispose( true );
            //Prevent the GC to call Finalize again, since you have already
            //cleaned up.
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            //Make sure Dispose does not get called more than once,
            //by checking the disposed field
            if ( !this.disposed )
            {
                if ( disposing )
                {
                    //Clean up managed resources
                    _watcher.Remove( _handler );
                }
                //Now clean up unmanaged resources
            }
            disposed = true;
        }

        #endregion
    }
}
