using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Application.Services;
using GICApp.Infrastructure.Persistence;
using GICApp.Infrastructure.Persistence.Repositories;
using GICApp.Infrastructure.Persistence.Service;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GICApp
{

   public class Program
    {
         
        public static void Main(string[] args)
        {
             
            bool exit = false;

            while (!exit)
            {

                MenuManager.ShowMenu(); 
                Console.Write("Please choose an option: ");
                string input = Console.ReadLine(); 
                exit = MenuManager.HandleInput(input);
                 

            }
             
        } 
      
    }
}