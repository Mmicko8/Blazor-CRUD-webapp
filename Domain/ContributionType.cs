namespace MangaProject.BL.Domain
{
    /// <summary>
    /// An author can contribute to a manga by drawing the art, writing the story or both.
    /// Usually a manga written by 2 authors divides the work so that one author writes the story and the other draws it.
    /// </summary>
    public enum ContributionType
    {
        Art,
        Story,
        Both
    }
}