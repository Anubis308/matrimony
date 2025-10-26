using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Services;
using System.Security.Claims;

namespace MatrimonyAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _messageService.SendMessageAsync(userId, request);

        if (result == null)
        {
            return BadRequest(new { message = "Failed to send message. No connection found or user not found." });
        }

        return Ok(result);
    }

    [HttpGet("conversations")]
    public async Task<IActionResult> GetConversations()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _messageService.GetConversationsAsync(userId);
        return Ok(result);
    }

    [HttpGet("conversation/{otherUserId}")]
    public async Task<IActionResult> GetConversation(string otherUserId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _messageService.GetConversationAsync(userId, otherUserId);
        return Ok(result);
    }

    [HttpPut("{messageId}/read")]
    public async Task<IActionResult> MarkAsRead(string messageId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _messageService.MarkAsReadAsync(userId, messageId);

        if (!result)
        {
            return BadRequest(new { message = "Failed to mark message as read" });
        }

        return Ok(new { message = "Message marked as read" });
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var count = await _messageService.GetUnreadCountAsync(userId);
        return Ok(new { unreadCount = count });
    }
}

