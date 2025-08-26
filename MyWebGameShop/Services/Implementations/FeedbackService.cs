using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Services.Implementations;

public class FeedbackService : IFeedbackService
{
    private readonly AppDbContext _context;

    public FeedbackService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddFeedbackAsync(FeedBackViewModel feedback)
    {
        var entity = new Feedback
        {
            From = feedback.From,
            Text = feedback.Text
        };

        _context.Feedbacks.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FeedBackViewModel>> GetAllFeedbacksAsync()
    {
        return await _context.Feedbacks
            .Select(f => new FeedBackViewModel
            {
                From = f.From,
                Text = f.Text
            })
            .ToListAsync();
    }
}