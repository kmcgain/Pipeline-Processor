namespace Pipeline.Model
{
    using Extensions;

    public class EntityIdentifier
    {
        private string resource;

        public EntityIdentifier(string resource)
        {
            this.resource = resource.Replace(" ", "").ToLowerInvariant();
        }

        public string RootRelativeResource()
        {
            return "/{0}".WithParams(ToString());
        }

        public override string ToString()
        {
            return resource;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            var objAsType = obj as EntityIdentifier;
            return objAsType != null && resource.Equals(objAsType.resource);
        }


        // override object.GetHashCode
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}