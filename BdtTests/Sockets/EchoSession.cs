// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Net.Sockets;
using System.Threading;

using Bdt.Shared.Logs;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.Tests.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Session de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class EchoSession : LoggedObject
    {

        #region " Constantes "
        // La taille du buffer d'IO
        public const int BUFFER_SIZE = 65536;
        // La durée minimale entre deux tests de l'état de connexion
        public const int STATE_POLLING_MIN_TIME = 10;
        // La durée maximale entre deux tests de l'état de connexion
        public const int STATE_POLLING_MAX_TIME = 5000;
        // Le coefficient de décélération,
        public const double STATE_POLLING_FACTOR = 1.1;
        // Le test de la connexion effective
        public const int SOCKET_TEST_POLLING_TIME = 100;
        #endregion

        #region " Attributs "
        protected TcpClient m_client;
        protected NetworkStream m_stream;
        protected ManualResetEvent m_mre = new ManualResetEvent(false);
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="client">le socket client pour la communication locale</param>
        /// -----------------------------------------------------------------------------
        public EchoSession(TcpClient client)
        {
            m_client = client;
            m_stream = client.GetStream();

            Thread thr = new Thread(new System.Threading.ThreadStart(CommunicationThread));
            thr.IsBackground = true;
            thr.Start();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="ex">l'exception à gérer</param>
        /// <param name="show">affichage du message d'erreur</param>
        /// -----------------------------------------------------------------------------
        protected void HandleError(Exception ex, bool show)
        {
            HandleError(ex.Message, show);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="message">le message à gérer</param>
        /// <param name="show">affichage du message d'erreur</param>
        /// -----------------------------------------------------------------------------
        protected void HandleError(string message, bool show)
        {
            if (show)
            {
                Log(message, ESeverity.ERROR);
            }
            m_mre.Set();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Calcule le temps d'attente nécessaire entre deux traitements
        /// </summary>
        /// <param name="polltime">l'attente entre pollings</param>
        /// <param name="adjpolltime">ajustement (durée du dernier aller-retour</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        protected int WaitTime(int polltime, int adjpolltime)
        {
            if (adjpolltime > polltime)
            {
                return 0;
            }
            else
            {
                return Math.Max(polltime - adjpolltime, STATE_POLLING_MIN_TIME);
            }
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal du thread
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void CommunicationThread()
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int polltime = STATE_POLLING_MIN_TIME;
            int adjpolltime = 0;

            while (!m_mre.WaitOne(WaitTime(polltime, adjpolltime), false))
            {
                DateTime startmarker = DateTime.Now;

                // Si des données sont présentes sur le socket, renvoi
                bool isConnected = false;
                bool isDataAvailAble = false;

                try
                {
                    isConnected = (!(m_client.Client.Poll(SOCKET_TEST_POLLING_TIME, System.Net.Sockets.SelectMode.SelectRead) && m_client.Client.Available == 0));
                    isDataAvailAble = m_stream.DataAvailable;
                }
                catch (Exception ex)
                {
                    HandleError(ex, false);
                }

                if (isConnected)
                {
                    if (isDataAvailAble)
                    {
                        int count = 0;
                        try
                        {
                            count = m_stream.Read(buffer, 0, BUFFER_SIZE);
                        }
                        catch (Exception ex)
                        {
                            HandleError(ex, true);
                        }
                        if (count > 0)
                        {
                            try
                            {
                                m_stream.Write(buffer, 0, count);
                                m_stream.Flush();
                            }
                            catch (Exception ex)
                            {
                                HandleError(ex, true);
                            }
                            // Si des données sont présentes, on repasse en mode 'actif'
                            polltime = STATE_POLLING_MIN_TIME;
                        }
                    }
                    else
                    {
                        // Sinon on augmente le temps de latence
                        polltime = Convert.ToInt32(Math.Round(STATE_POLLING_FACTOR * polltime));
                        polltime = Math.Min(polltime, STATE_POLLING_MAX_TIME);
                    }
                }
                else
                {
                    // Deconnexion
                    m_mre.Set();
                }
                adjpolltime = Convert.ToInt32(DateTime.Now.Subtract(startmarker).TotalMilliseconds);
            }
            Disconnect();
        }
       

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deconnexion
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Disconnect()
        {
            if (m_client != null)
            {
                m_stream.Close();
                m_client.Close();
                m_stream = null;
                m_client = null;
            }
        }
        #endregion

    }

}

