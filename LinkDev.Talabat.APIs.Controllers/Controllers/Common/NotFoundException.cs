namespace LinkDev.Talabat.APIs.Controllers.Controllers.Common
{
	public class NotFoundException : ApplicationException
	{
        public NotFoundException()
            :base ("Not Found ")
        {
            
        }
    }
}
