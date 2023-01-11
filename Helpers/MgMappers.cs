using AutoMapper;

namespace MachManager.Helpers{
    public static class MgMappers
    {
        public static V MapTo<T, V>(this T from, V to)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<T, V>();
            });

            IMapper iMapper = config.CreateMapper();
            iMapper.Map<T, V>(from, to);

            return to;
        }

        public static int ToInt32(this string text)
        {
            return Convert.ToInt32(text);
        }
    }
}