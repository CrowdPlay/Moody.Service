﻿namespace Moody.Service.Converters
{
    public interface IConverter<T, T1>
    {
        T1 Convert(T candidate);
    }
}