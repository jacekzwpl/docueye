using AutoMapper;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ViewsExploder
    {
        private readonly IMapper mapper;

        public ViewsExploder(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
