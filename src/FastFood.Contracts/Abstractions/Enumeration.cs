using System.Reflection;

namespace FastFood.Contracts.Abstractions;

public class Enumeration<TValue, TDescription>
    where TValue : class
    where TDescription : class
{
    public TValue Value { get; }
    public TDescription Description { get; }

    public Enumeration(TValue value, TDescription description)
    {
        Value = value;
        Description = description;
    }
    
    public static IReadOnlyCollection<T> GetAll<T>()
        where T : Enumeration<TValue, TDescription>
    {
        Type abstractType = typeof(T);
        return abstractType
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(p => p.GetValue(abstractType))
            .OfType<T>()
            .ToList()
            .AsReadOnly();
    }

    public static T? FromValue<T>(TValue value)
        where T : Enumeration<TValue, TDescription> =>
        GetAll<T>().FirstOrDefault(item => item.Value.Equals(value));
    
    public static bool Exists<T>(TValue value) 
        where T : Enumeration<TValue, TDescription> => 
        GetAll<T>().Any(item => item.Value == value);
    
    public static implicit operator TValue(Enumeration<TValue, TDescription> enumeration) => enumeration.Value;
}