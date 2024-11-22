namespace LinkDev.Talabat.Core.Application.Exception
{
	public class UnAuthorizedException : ApplicationException
	{
        public UnAuthorizedException(string message )
            :base (message) 
        {
            
        }
    }
}
