using System.Runtime.Serialization;

namespace Veseta.Core.entites
{
    public enum BookingStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Canceled")]
        Canceled
    }
}