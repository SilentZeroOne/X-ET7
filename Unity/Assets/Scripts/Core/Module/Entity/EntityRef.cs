using System;

namespace ET
{
    public readonly struct EntityRef<T> where T: Entity
    {
        private readonly long instanceId;
        private readonly T entity;

        private EntityRef(T t)
        {
            if (t != null)
            {
                this.instanceId = t.InstanceId;
                this.entity = t;
            }
            else
            {
                this.instanceId = 0;
                this.entity = null;
            }
        }
        
        private T UnWrap
        {
            get
            {
                if (this.entity == null)
                {
                    return null;
                }
                if (this.entity.InstanceId != this.instanceId)
                {
                    return null;
                }
                return this.entity;
            }
        }
        
        public static implicit operator EntityRef<T>(T t)
        {
            return new EntityRef<T>(t);
        }

        public static implicit operator T(EntityRef<T> v)
        {
            return v.UnWrap;
        }
    }
}