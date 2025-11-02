namespace Domain.Models;

// Parent for all Entities {Domain Models} => {SQL Server DB } => StoreDbContext
public class BaseEntity<TKey>
{
    public TKey Id { get; set; } // PK 
}
