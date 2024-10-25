namespace LinkDev.Talabat.Infrastructure.Presistance._Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContextTypeAttribute : Attribute
    {
        public Type DbContextType { get; }

        public DbContextTypeAttribute(Type dbContextType)
        {
            DbContextType = dbContextType;
        }
    }
}
