// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Protocole de communication basé sur le remoting .NET et sur le protocole HTTPs
    /// Utilise un formateur binaire pour les données
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class HttpSslBinaryRemoting : HttpBinaryRemoting
    {

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Utiliser le cryptage SSL
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override bool UseSSL
        {
            get
            {
                return true;
            }
        }
        #endregion

    }

}

