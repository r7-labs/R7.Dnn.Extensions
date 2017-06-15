//
//  UrlHistoryInProcBackend.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace R7.Dnn.Extensions.UrlHistory
{
    /// <summary>
    /// UrlHistory backend for InProc sessions
    /// </summary>
    public class UrlHistoryInProcBackend : UrlHistoryBackend
    {
        HttpSessionState _session;

        string _variableName;

        public override void Init (HttpSessionState session, string variableName)
        {
            _session = session;
            _variableName = variableName;
        }

        public override void StoreUrl (string url)
        {
            var sessionObject = _session [_variableName];
            if (sessionObject != null) {
                var urlList = (IList<string>) sessionObject;
                var index = urlList.IndexOf (url);
                if (index >= 0) {
                    urlList.RemoveAt (index);
                }
                urlList.Insert (0, url);
                _session [_variableName] = urlList;
            }
            else {
                _session [_variableName] = new List<string> { url };
            }
        }

        public override IEnumerable<string> GetUrls ()
        {
            var sessionObject = _session [_variableName];
            if (sessionObject != null) {
                return (IEnumerable<string>) sessionObject;
            }

            return Enumerable.Empty<string> ();
        }
    }
}
