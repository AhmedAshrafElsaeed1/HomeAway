//using home_away.Interfaces;
//using HomeAway.DTOs;
//using HomeAway.Models;

//public class HotelService : IHotelService
//{
//    private readonly ApplicationDbContext _context;

//    public HotelService(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // GET ALL
//    public async Task<IEnumerable<HotelDto>> GetAllAsync()
//    {
//        return await _context.Hotels
//            .Select(h => new HotelDto
//            {
//                Id = h.Id,
//                Name = h.Name,
//                Address = h.Address
//            })
//            .ToListAsync();
//    }

//    // GET BY ID
//    public async Task<HotelDto> GetByIdAsync(int id)
//    {
//        var h = await _context.Hotels.FindAsync(id);

//        if (h == null) return null;

//        return new HotelDto
//        {
//            Id = h.Id,
//            Name = h.Name,
//            Address = h.Address
//        };
//    }

//    // CREATE
//    public async Task<int> CreateAsync(HotelDto dto)
//    {
//        var hotel = new Hotel
//        {
//            Name = dto.Name,
//            Address = dto.Address
//        };

//        _context.Hotels.Add(hotel);
//        await _context.SaveChangesAsync();
//        return hotel.Id;
//    }

//    // UPDATE
//    public async Task<bool> UpdateAsync(HotelDto dto)
//    {
//        var hotel = await _context.Hotels.FindAsync(dto.Id);
//        if (hotel == null)
//            return false;

//        hotel.Name = dto.Name;
//        hotel.Address = dto.Address;

//        _context.Hotels.Update(hotel);
//        await _context.SaveChangesAsync();
//        return true;
//    }

//    // DELETE
//    public async Task<bool> DeleteAsync(int id)
//    {
//        var hotel = await _context.Hotels.FindAsync(id);
//        if (hotel == null)
//            return false;

//        _context.Hotels.Remove(hotel);
//        await _context.SaveChangesAsync();
//        return true;
//    }
//}