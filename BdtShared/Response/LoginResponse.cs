/* BoutDuTunnel Copyright (c)  2007-2013 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region " Inclusions "
using System;
#endregion

namespace Bdt.Shared.Response
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une r�ponse g�n�rique de login
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public struct LoginResponse : IMinimalResponse 
    {

		#region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La requ�te a aboutie/�chou� ?
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool Success { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le message d'information
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string Message { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le jeton de session
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int Sid { get; private set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="success">Login r�ussi/�chou�</param>
        /// <param name="message">Le message d'information</param>
        /// <param name="sid">Le jeton de session affect�</param>
        /// -----------------------------------------------------------------------------
        public LoginResponse(bool success, string message, int sid) : this()
        {
            Success = success;
            Message = message;
            Sid = sid;
        }
        #endregion

    }

}


