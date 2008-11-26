// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Runtime.Remoting.Channels.Http;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Protocole de communication basé sur le remoting .NET et sur le protocole HTTPs
    /// Utilise un formateur SOAP pour les données
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class HttpSslSoapRemoting : HttpSoapRemoting 
    {

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Utiliser le cryptage SSL
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override bool IsSecured
        {
            get
            {
                return true;
            }
        }
        #endregion

    }

}

