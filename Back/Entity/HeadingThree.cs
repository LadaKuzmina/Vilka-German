﻿namespace Entity
{
    public class HeadingThree
    {
        public string Title { get; set; }
        public string? PageLink { get; set; }
        public string? ImageRef { get; set; }
        public string HeadingTwoTitle { get; set; }

        public HeadingThree(string title, string headingTwoTitle, string? imageRef, string? pageLink)
        {
            Title = title;
            HeadingTwoTitle = headingTwoTitle;
            ImageRef = imageRef;
            PageLink = pageLink;
        }
    }
}