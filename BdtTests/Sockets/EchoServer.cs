// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System.Net.Sockets;

using Bdt.Shared.Logs;
using Bdt.Shared.Service;
using Bdt.Client.Resources;
using Bdt.Client.Sockets;
#endregion

namespace Bdt.Tests.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Serveur de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class EchoServer : TcpServer
    {

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="localport">port local côté client</param>
        /// <param name="shared">bind sur toutes les ip/ip locale</param>
        /// -----------------------------------------------------------------------------
        public EchoServer(int localport, bool shared)
            : base(localport, shared)
        {
            Log(string.Format("Echo server listenning {0}:{1}", Ip, localport), ESeverity.INFO);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Callback en cas de nouvelle connexion
        /// </summary>
        /// <param name="client">le socket client</param>
        /// -----------------------------------------------------------------------------
        protected override void OnNewConnection(TcpClient client)
        {
            new EchoSession(client);
        }
        #endregion

    }

}

