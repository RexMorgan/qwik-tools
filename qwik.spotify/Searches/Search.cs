using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using qwik.spotify.Errors;

namespace qwik.spotify.Searches
{
    public class Search : IDisposable
    {
        private bool _disposed;
        private IntPtr _searchPtr;
        private IntPtr _callbackPtr;
        private SearchCompleteCallbackDelegate _delegate;

        public delegate void SearchCompleteCallbackDelegate(IntPtr result, IntPtr userDataPtr);

        public event Action<Search> SearchCompleted;

        public bool IsLoaded { get; private set; }

        public List<IntPtr> TrackPtrs { get; private set; }
        public List<IntPtr> AlbumPtrs { get; private set; }
        public List<IntPtr> ArtistPtrs { get; private set; }

        public static Search NewSearch(string keywords, IntPtr sessionPtr)
        {
            try
            {
                var search = new Search();
                search._delegate = search.SearchComplete;
                search._callbackPtr = Marshal.GetFunctionPointerForDelegate(search._delegate);
                search._searchPtr = Externals.sp_search_create(
                    sessionPtr,
                    keywords.ToPtr(),
                    0, 50,
                    0, 50,
                    0, 50,
                    0, 0,
                    SearchType.Standard,
                    search._callbackPtr,
                    IntPtr.Zero);
                return search;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SearchComplete(IntPtr result, IntPtr userDataPtr)
        {
            if (_searchPtr == null) throw new InvalidOperationException("Search pointer is null");

            var error = Externals.sp_search_error(_searchPtr);
            if (error != ErrorCode.OK)
            {
                IsLoaded = true;
                return;
            }

            TrackPtrs = Enumerable.Range(0, Externals.sp_search_num_tracks(_searchPtr))
                                  .Select(x => Externals.sp_search_track(_searchPtr, x))
                                  .ToList();

            ArtistPtrs = Enumerable.Range(0, Externals.sp_search_num_artists(_searchPtr))
                                   .Select(x => Externals.sp_search_artist(_searchPtr, x))
                                   .ToList();

            AlbumPtrs = Enumerable.Range(0, Externals.sp_search_num_albums(_searchPtr))
                                  .Select(x => Externals.sp_search_album(_searchPtr, x))
                                  .ToList();

            IsLoaded = true;

            if (SearchCompleted != null) SearchCompleted(this);
        }

        protected Search()
        {

        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Search()
        {
            dispose(false);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    safeReleaseSearch();
                }
                _disposed = true;
            }
        }

        private void safeReleaseSearch()
        {
            if (_searchPtr != IntPtr.Zero)
            {
                try
                {
                    Externals.sp_search_release(_searchPtr);
                }
                catch { }
                finally
                {
                    _searchPtr = IntPtr.Zero;
                }
            }
        }
    }
}