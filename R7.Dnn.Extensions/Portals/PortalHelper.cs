using System.Linq;
using DotNetNuke.Entities.Portals;

namespace R7.Dnn.Extensions.Portals
{
    public class PortalHelper
    {
        /// <summary>
        /// Gets the portal settings with portal alias outside HTTP context.
        /// Could be useful along with e.g. Globals.NavigateURL().
        /// </summary>
        /// <returns>The portal settings.</returns>
        /// <param name="portalId">Portal identifier.</param>
        /// <param name="tabId">Tab identifier.</param>
        /// <param name="cultureCode">Culture code.</param>
        public PortalSettings GetPortalSettingsOutOfHttpContext (int portalId, int tabId, string cultureCode)
        {
            var portalAlias = GetPortalAliasOutOfHttpContext (portalId, tabId, cultureCode);
            return portalAlias != null ? new PortalSettings (tabId, portalAlias) : null;
        }
        
        /// <summary>
        /// Gets the portal alias outside HTTP context.
        /// Could be useful along with e.g. Globals.NavigateURL().
        /// </summary>
        /// <returns>The portal alias.</returns>
        /// <param name="portalId">Portal identifier.</param>
        /// <param name="tabId">Tab identifier.</param>
        /// <param name="cultureCode">Culture code.</param>
        public PortalAliasInfo GetPortalAliasOutOfHttpContext (int portalId, int tabId, string cultureCode)
        {
            var portalAliases = PortalAliasController.Instance.GetPortalAliasesByPortalId (portalId).ToList ();
            var portalSettings = new PortalSettings (portalId);
            var portalAlias = default (PortalAliasInfo);

            if (!string.IsNullOrEmpty (cultureCode)) {
                portalAlias = portalAliases.FirstOrDefault (pa => pa.IsPrimary && pa.CultureCode == cultureCode);
                if (portalAlias == null) {
                    portalAlias = portalAliases.FirstOrDefault (pa => pa.CultureCode == cultureCode);
                }
            }
            else {
                portalAlias = portalAliases.FirstOrDefault (pa =>
                    pa.IsPrimary && pa.CultureCode == portalSettings.DefaultLanguage);
                if (portalAlias == null) {
                    portalAlias = portalAliases.FirstOrDefault (pa => pa.IsPrimary && pa.CultureCode == "");
                }
            }

            if (portalAlias == null) {
                portalAlias = portalAliases.FirstOrDefault (pa => pa.IsPrimary);
                if (portalAlias == null) {
                    portalAlias = portalAliases.FirstOrDefault ();
                }
            }

            return portalAlias;
        }
    }
}