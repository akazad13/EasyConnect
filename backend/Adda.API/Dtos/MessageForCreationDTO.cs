using System;

namespace Adda.API.Dtos;

public class MessageForCreationDto
{
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public string Content { get; set; }
}