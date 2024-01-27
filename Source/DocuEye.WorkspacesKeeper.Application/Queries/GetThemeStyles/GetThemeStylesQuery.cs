using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetThemeStyles
{
    /// <summary>
    /// Gets the theme style from given url
    /// </summary>
    public class GetThemeStylesQuery : IRequest<ThemeStylesResult?>
    {
        /// <summary>
        /// The theme url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="url">The theme url</param>
        public GetThemeStylesQuery(string url)
        {
            this.Url = url;
        }
    }
}
