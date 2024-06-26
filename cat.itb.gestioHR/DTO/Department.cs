﻿namespace cat.itb.gestioHR.DTO
{
    public class Department
    {
        public virtual int _id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Loc { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(_id)}={_id.ToString()}, {nameof(Name)}={Name}, {nameof(Loc)}={Loc}}}";
        }
    }
}

