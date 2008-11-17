// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
#endregion

namespace Bdt.Shared.Request
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une demande de suppression d'une session
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct KillSessionRequest : ISessionContextRequest 
    {

        #region " Attributs "
        private int m_sid;
        private int m_adminsid;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de session
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int Sid
        {
            get
            {
                return m_sid;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de session admin
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int AdminSid
        {
            get
            {
                return m_adminsid;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sid">Le jeton de session</param>
        /// <param name="adminsid">Le jeton de session admin</param>
        /// -----------------------------------------------------------------------------
        public KillSessionRequest(int sid, int adminsid)
        {
            this.m_sid = sid;
            this.m_adminsid = adminsid;
        }
        #endregion

    }

}

