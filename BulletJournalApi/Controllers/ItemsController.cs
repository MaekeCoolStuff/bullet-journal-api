using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BulletJournal.Domain;
using BulletJournalApi.Models;
using BulletJournalApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulletJournalApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ItemModel>> Get()
        {
            List<Item> items = _itemService.Get();

            var itemModels = _mapper.Map<List<ItemModel>>(items);
            return itemModels;
        }
            

        [HttpGet("{id:length(24)}", Name = "GetItem")]
        public ActionResult<ItemModel> Get(int id)
        {
            var item = _itemService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return _mapper.Map<ItemModel>(item);
        }

        [HttpPost]
        public ActionResult<ItemModel> Create(ItemModel itemModel)
        {
            List<int> date = itemModel.Date.Split("-").Select(int.Parse).ToList();
            Item item = new Item
            {
                UserId = itemModel.UserId,
                Status = itemModel.Status,
                Text = itemModel.Text,
                Type = itemModel.Type,
                Date = new DateTime(date[2], date[1], date[0])
            };

            _itemService.Create(item);

            return _mapper.Map<ItemModel>(item);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(int id, Item itemIn)
        {
            var item = _itemService.Get(id);

            if(item == null)
            {
                return NotFound();
            }

            _itemService.Update(itemIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(int id)
        {
            var itemToDelete = _itemService.Get(id);

            if(itemToDelete == null)
            {
                return NotFound();
            }

            _itemService.Remove(itemToDelete);

            return NoContent();
        }


    }
}