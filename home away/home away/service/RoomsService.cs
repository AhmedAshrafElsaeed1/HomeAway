//namespace HomeAway.Services
//{


//    public class RoomService : IRoomService
//    {
//        private readonly ApplicationDbContext _context;

//        public RoomService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // =============================
//        // GET ALL ROOMS
//        // =============================
//        public async Task<IEnumerable<RoomDto>> GetAllAsync()
//        {
//            return await _context.Rooms
//                .Select(r => new RoomDto
//                {
//                    Id = r.Id,
//                    Number = r.Number,
//                    HotelId = r.HotelId,
//                    PricePerNight = r.PricePerNight,
//                    IsAvailable = r.IsAvailable
//                })
//                .ToListAsync();
//        }

//        // =============================
//        // GET ROOM BY ID
//        // =============================
//        public async Task<RoomDto> GetRoomByIdAsync(int id)
//        {
//            var r = await _context.Rooms.FindAsync(id);

//            if (r == null) return null;

//            return new RoomDto
//            {
//                Id = r.Id,
//                Number = r.Number,
//                HotelId = r.HotelId,
//                PricePerNight = r.PricePerNight,
//                IsAvailable = r.IsAvailable
//            };
//        }

//        // =============================
//        // CREATE ROOM
//        // =============================
//        public async Task<int> CreateRoomAsync(CreateRoomDto dto)
//        {
//            var room = new Room
//            {
//                Number = dto.Number,
//                HotelId = dto.HotelId,
//                PricePerNight = dto.PricePerNight,
//                IsAvailable = dto.IsAvailable
//            };

//            _context.Rooms.Add(room);
//            await _context.SaveChangesAsync();

//            return room.Id;
//        }

//        // =============================
//        // UPDATE ROOM
//        // =============================
//        public async Task<RoomDto> UpdateAsync(RoomDto dto)
//        {
//            var room = await _context.Rooms.FindAsync(dto.Id);

//            if (room == null) return null;

//            room.Number = dto.Number;
//            room.HotelId = dto.HotelId;
//            room.PricePerNight = dto.PricePerNight;
//            room.IsAvailable = dto.IsAvailable;

//            await _context.SaveChangesAsync();

//            return dto;
//        }

//        // =============================
//        // DELETE ROOM
//        // =============================
//        public async Task<bool> DeleteAsync(int id)
//        {
//            var room = await _context.Rooms.FindAsync(id);

//            if (room == null) return false;

//            _context.Rooms.Remove(room);
//            await _context.SaveChangesAsync();

//            return true;
//        }
//    }
//}