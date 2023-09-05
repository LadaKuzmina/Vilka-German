using System.Collections.Generic;

namespace Entity
{
    public class HeadingOne
    {
        public string Title { get; set; }
        public string? PageLink { get; set; }
        public string? ImageRef { get; set; }

        private readonly int _hashValue;

        public HeadingOne(string title, string? pageLink)
        {
            Title = title;
            PageLink = pageLink;
        
            _hashValue = GetFNVHashCode();
        }
        
        public HeadingOne(string title, string? pageLink, string? imageRef)
        {
            Title = title;
            PageLink = pageLink;
            ImageRef = imageRef;

            _hashValue = GetFNVHashCode();
        }
        
        private int GetFNVHashCode()
        {
            unchecked
            {
                long p = 16777619;
                long hash = 2166136261;
                var propertiesValues = GetType().GetProperties()
                    .Where(property => property.Name != "_hashValue")
                    .Select(property => property.GetValue(this, null));
                
                foreach (var propertiesValue in propertiesValues)
                {
                    if (propertiesValue == null) continue;
                    hash ^= propertiesValue.GetHashCode();
                    hash *= p;
                }
                
                return (int)hash;
            }
        }
        
        public override int GetHashCode() => _hashValue;

        public override bool Equals(object? obj)
        {
            if (obj is not HeadingOne objHeadingOne)
            {
                return false;
            }
            
            var thisPropertiesValues = GetType().GetProperties()
                .Where(property => property.Name != "_hashValue")
                .Select(property => property.GetValue(this, null)).ToList();
            var otherPropertiesValues = objHeadingOne.GetType().GetProperties()
                .Where(property => property.Name != "_hashValue")
                .Select(property => property.GetValue(objHeadingOne, null)).ToList();
            
            var areEqual = true;
            for (var i = 0; i < thisPropertiesValues.Count; i++)
            {
                if (thisPropertiesValues[i] == null && otherPropertiesValues[i] == null) continue;
                
                if ((thisPropertiesValues[i] == null && otherPropertiesValues[i] != null)
                    || (thisPropertiesValues[i] != null && otherPropertiesValues[i] == null)
                    || !thisPropertiesValues[i]!.Equals(otherPropertiesValues[i]))
                {
                    areEqual = false;
                    break;
                }
            }
            
            return areEqual;
        }
    }
}