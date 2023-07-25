using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_core_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_core_MVC.Controllers
{
    public class MoviesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }



        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // idやMovieテーブルがnullの場合はNotFoundを返す
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            // Movieテーブルからidが一致する映画データを取得し、ビューに返す
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // Edit画面で「Save」をクリックした際に実行される
        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Revenue,ReleaseDate")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            // モデル検証が成功した場合の処理
            if (ModelState.IsValid)
            {
                try
                {
                    // 送信されたデータをもとにMovieテーブルのデータを更新
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                // 同時に同じデータが変更された（競合が発生した）場合の例外処理
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Indexページにリダイレクトする
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            // Movieテーブルから全てのデータを取得するLINQクエリ
            var movies = _context.Movie.Select(m => m);

            // タイトル検索処理
            if (!string.IsNullOrEmpty(searchString))
            {
                // タイトルに検索文字列が含まれるデータを抽出するLINQクエリ
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            // ToListAsyncメソッドが呼び出されたらクエリが実行され(遅延実行)、その結果をビューに返す
            return View(await movies.ToListAsync());
        }
    }
}

