using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelMVCTest2.DBUtil;
using HotelMVCTest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVCTest2.Controllers
{
    public class HotelController : Controller
    {

        private readonly ManageHotel _manageHotel;

        public HotelController()
        {
            _manageHotel = new ManageHotel();;
        }
        

        public IActionResult Index()
        {
            return View( _manageHotel.Get() );
        }


        //GEY -CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)  //validering på serverside
            {
                //if valid
                bool ok = _manageHotel.Post(hotel);

                //if (!ok)
                //{
                //    //show errormessage
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel =  _manageHotel.Get(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _manageHotel.Put(hotel.Id, hotel);
                
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel = _manageHotel.Get(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hotel =  _manageHotel.Get(id);  
            if (hotel == null)
            {
                return View();
            }
            bool ok= _manageHotel.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //GET - DETAILS
        public  IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel =  _manageHotel.Get(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

    }
}