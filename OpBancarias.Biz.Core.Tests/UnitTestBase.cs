using AutoMapper;
using OpBancarias.Core.Biz;
using System.Reflection;
using System.Security.Claims;

namespace OpBancarias.Biz.Core.Tests
{
    // Base class for unit tests
    public class UnitTestBase
    {
        public ClaimsPrincipal Principal;

        public Application Application;

        public IMapper Mapper;


        public UnitTestBase()
        {
            Application = new Application();
            Mapper = GetMapper();
        }

        protected virtual MapperConfigurationExpression GetMapperConfig()
        {
            List<Type> listOfTypes = new List<Type>();
            List<Assembly> listOfAssemblies = new List<Assembly>();
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
            string[] files = Directory.GetFiles(directoryName, "OpBancarias.*.dll");
            string[] array = files;
            foreach (string path in array)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                listOfAssemblies.Add(Assembly.Load(fileNameWithoutExtension));
            }

            if (listOfAssemblies != null)
            {
                foreach (Assembly item in listOfAssemblies)
                {
                    listOfTypes.AddRange(item.GetTypes());
                }
            }

            MapperConfigurationExpression mapperConfigurationExpression = new MapperConfigurationExpression();

            foreach (Type itemType in listOfTypes)
            {
                if (itemType.IsSubclassOf(typeof(Profile)))
                {
                    mapperConfigurationExpression.AddProfile(itemType);

                }
            }

            return mapperConfigurationExpression;
        }

        protected virtual IMapper GetMapper()
        {
            MapperConfigurationExpression mapperConfig = GetMapperConfig();
            MapperConfiguration configurationProvider = new MapperConfiguration(mapperConfig);
            return new Mapper(configurationProvider);
        }
    }
}
