﻿using Microsoft.AspNetCore.Mvc;
using RehabCV.DTO;
using RehabCV.Extension;
using RehabCV.Models;
using RehabCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehabCV.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroup<Group> _group;
        private readonly INumberOfCh<NumberOfChildren> _numberOfCh;

        public GroupController(IGroup<Group> group,
                               INumberOfCh<NumberOfChildren> numberOfCh)
        {
            _group = group;
            _numberOfCh = numberOfCh;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _group.FindAll();

            return View(groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(GroupDTO groupDTO)
        {
            if (ModelState.IsValid)
            {
                var numberOfChildren = await _numberOfCh.GetNumber();

                var group = new Group
                {
                    Id = Guid.NewGuid().ToString(),
                    NameOfDisease = groupDTO.NameOfDisease
                };

                group = group.DivisionChildrenInGroups(numberOfChildren.NumberOfChildrenInGroup);

                var result = await _group.CreateAsync(group);

                if (result != null)
                {
                    return RedirectToAction("Index", "Group");
                }
            }

            return View(groupDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var group = await _group.FindById(id);

            var groupDTO = new GroupDTO
            {
                NameOfDisease = group.NameOfDisease
            };

            return View(groupDTO);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _group.DeleteAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Group");
        }
    }
}