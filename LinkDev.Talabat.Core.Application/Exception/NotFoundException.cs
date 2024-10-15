namespace LinkDev.Talabat.Core.Application.Exception
{
	public class NotFoundException : ApplicationException
	{
        public NotFoundException( string name , object key  )
            :base($"{name} with ({key}) is not found ")
        {
            
        }
    }
}
