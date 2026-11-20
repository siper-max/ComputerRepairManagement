
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerRepairManagement.Data;
using ComputerRepairManagement.Models.Database;

namespace ComputerRepairManagement.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách khách hàng
        public async Task<IActionResult> Index()
        {
            var danhSach = await _context.Set<KhachHang>()
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(danhSach);
        }

        // Thêm hoặc sửa khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(
            int id, string? hoTen, string? soDienThoai)
        {
            hoTen = hoTen?.Trim();
            soDienThoai = soDienThoai?.Trim();

            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(soDienThoai) ||
                hoTen.Length > 100 ||
                soDienThoai.Length > 20)
            {
                TempData["Error"] = "Thông tin không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            if (id == 0)
            {
                var khachHang = new KhachHang
                {
                    HoTen = hoTen,
                    SoDienThoai = soDienThoai,
                    NgayTao = DateTime.Now
                };

                _context.Add(khachHang);
            }
            else
            {
                var khachHang = await _context
                    .Set<KhachHang>().FindAsync(id);

                if (khachHang == null)
                    return NotFound();

                khachHang.HoTen = hoTen;
                khachHang.SoDienThoai = soDienThoai;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Xóa khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var khachHang = await _context
                .Set<KhachHang>().FindAsync(id);

            if (khachHang == null)
                return NotFound();

            _context.Remove(khachHang);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
