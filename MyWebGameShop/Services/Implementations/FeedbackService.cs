using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models; // ✅ ТОЛЬКО Models
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class FeedbackService : IFeedbackService
{
    private readonly AppDbContext _context;

    public FeedbackService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
    {
        feedback.CreatedAt = DateTime.Now; // Устанавливаем дату создания
        
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return feedback;
    }

    public async Task<List<Feedback>> GetAllFeedbacksAsync()
    {
        return await _context.Feedbacks
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();
    }

    public async Task<Feedback?> GetFeedbackByIdAsync(int id)
    {
        return await _context.Feedbacks.FindAsync(id);
    }

    public async Task<bool> DeleteFeedbackAsync(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null) return false;

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Feedback>> GetRecentFeedbacksAsync(int count = 10)
    {
        return await _context.Feedbacks
            .OrderByDescending(f => f.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}