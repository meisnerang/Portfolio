using GuildCars.Data.Repository;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class TestsADO
    {
        [SetUp]
        public void init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepository();
            var bodyStyles = repo.GetAll();

            Assert.AreEqual(4, bodyStyles.Count());
            Assert.AreEqual("Car", bodyStyles[0].BodyStyleName);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepository();
            var colors = repo.GetAll();

            Assert.AreEqual(16, colors.Count());
            Assert.AreEqual("Beige", colors[15].ColorName);
        }

        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepository();
            var purchaseTypes = repo.GetAll();

            Assert.AreEqual(3, purchaseTypes.Count());
            Assert.AreEqual("Cash", purchaseTypes[1].PurchaseTypeName);
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepository();
            var states = repo.GetAll();

            Assert.AreEqual(4, states.Count());
            Assert.AreEqual("Indiana", states[0].StateName);
        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialRepository();
            var specials = repo.GetAll();

            Assert.AreEqual(4, specials.Count());
        }

        [Test]
        public void CanAddSpecial()
        {
            Special specialToAdd = new Special();
            var repo = new SpecialRepository();

            specialToAdd.Title = "Added Special Title";
            specialToAdd.Description = "Added Special Description";

            repo.Add(specialToAdd);

            Assert.AreEqual(5, specialToAdd.SpecialId);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            Special specialToAdd = new Special();
            var repo = new SpecialRepository();

            specialToAdd.Title = "Added Special Title";
            specialToAdd.Description = "Added Special Description";

            repo.Add(specialToAdd);

            var loaded = repo.GetById(5);
            Assert.IsNotNull(loaded);

            repo.Delete(5);
            loaded = repo.GetById(5);
            Assert.IsNull(loaded);
        }

        [Test]
        public void CanDeleteVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehicleRepository();

            vehicleToAdd.MakeId = 2;
            vehicleToAdd.ModelId = 5;
            vehicleToAdd.Year = 2015;
            vehicleToAdd.BodyStyleId = 1;
            vehicleToAdd.IsManualTransmission = false;
            vehicleToAdd.ExteriorColorId = 5;
            vehicleToAdd.InteriorColorId = 2;
            vehicleToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            vehicleToAdd.Mileage = 34000;
            vehicleToAdd.SalePrice = 27409;
            vehicleToAdd.MSRP = 30000;
            vehicleToAdd.Vin = "12121345678990909";
            vehicleToAdd.Description = "Test description";
            vehicleToAdd.PhotoFile = "";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsNew = false;
            vehicleToAdd.IsSold = false;

            repo.Add(vehicleToAdd);

            var loaded = repo.GetById(16);
            Assert.IsNotNull(loaded);

            repo.Delete(16);
            loaded = repo.GetById(16);
            Assert.IsNull(loaded);
        }

        [Test]
        public void CanAddContact()
        {
            Contact contactToAdd = new Contact();
            var repo = new ContactRepository();

            contactToAdd.Name = "Joaquim Diaz";
            contactToAdd.Email = "diaz@test.com";
            contactToAdd.Phone = "444-444-5656";
            contactToAdd.Message = "Is this car still available?";

            repo.Add(contactToAdd);

            Assert.AreEqual(7, contactToAdd.ContactId);
        }

        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactRepository();
            var contacts = repo.GetAll();

            Assert.AreEqual(6, contacts.Count());
        }

        [Test]
        public void CanAddMake()
        {
            Make makeToAdd = new Make();
            var repo = new MakeRepository();

            makeToAdd.MakeName = "Toyota";
            makeToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            makeToAdd.DateAdded = DateTime.Parse("2019/03/20");

            repo.Add(makeToAdd);

            Assert.AreEqual(5, makeToAdd.MakeId);
            Assert.AreEqual("Toyota", makeToAdd.MakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", makeToAdd.UserId);
            
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakeRepository();
            var makes = repo.GetAll();

            Assert.AreEqual(4, makes.Count());
        }

        [Test]
        public void CanLoadMakeItems()
        {
            var repo = new MakeRepository();
            var makes = repo.GetAllMakeItem();
            List<MakeItem> makesList = makes.ToList();

            Assert.AreEqual(4, makes.Count());
            Assert.AreEqual("Dodge", makesList[0].MakeName);
            Assert.AreEqual("tester1@gmail.com", makesList[0].Email);
        }

        [Test]
        public void CanAddModel()
        {
            Model modelToAdd = new Model();
            var repo = new ModelRepository();

            modelToAdd.ModelName = "CR-V";
            modelToAdd.MakeId = 4;
            modelToAdd.UserId = "00000000-0000-0000-0000-000000000000";

            repo.Add(modelToAdd);

            Assert.AreEqual(8, modelToAdd.ModelId);
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new ModelRepository();
            var models = repo.GetAll();
            List<ModelItem> modelList = models.ToList();

            Assert.AreEqual(7, models.Count());
            Assert.AreEqual("Edge", modelList[1].ModelName);
            Assert.AreEqual("Ford", modelList[1].MakeName);
            Assert.AreEqual("tester1@gmail.com", modelList[1].Email);
        }

        [Test]
        public void CanLoadModelByMake()
        {
            var repo = new ModelRepository();
            var models = repo.GetModelsByMake(1);

            Assert.AreEqual(4, models.Count());
        }

        [Test]
        public void CanLoadFeatured()
        {
            var repo = new VehicleRepository();
            List<FeaturedItem> featured = repo.GetFeatured().ToList();

            Assert.AreEqual(4, featured.Count());
            Assert.AreEqual(2, featured[0].VehicleId);
        }

        [Test]
        public void CanLoadVehicles()
        {
            var repo = new VehicleRepository();
            List<VehicleItem> vehicles = repo.GetAll().ToList();

            Assert.AreEqual(15, vehicles.Count());
        }

        [Test]
        public void CanLoadNewInventoryReport()
        {
            var repo = new VehicleRepository();
            List<InventoryReportItem> inventory = repo.GetNewInventory().ToList();

            Assert.AreEqual(4, inventory.Count());

        }

        [Test]
        public void CanLoadVehicleById()
        {
            var repo = new VehicleRepository();
            var vehicle = repo.GetById(4);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual(4, vehicle.VehicleId);
            Assert.AreEqual("inventory-4.jpg", vehicle.PhotoFile);
            Assert.AreEqual(2019, vehicle.Year);
            Assert.AreEqual("Subaru", vehicle.MakeName);
            Assert.AreEqual("Outback", vehicle.ModelName);
            Assert.AreEqual("Car", vehicle.BodyStyleName);
            Assert.AreEqual(false, vehicle.IsManualTransmission);
            Assert.AreEqual("Magnetite Grey", vehicle.ExteriorColor);
            Assert.AreEqual("Venetian Red Pearl", vehicle.InteriorColor);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual("123456789AAAAAAAD", vehicle.Vin);
            Assert.AreEqual("Sample data vehicle description", vehicle.Description);
            Assert.AreEqual(34535, vehicle.SalePrice);
            Assert.AreEqual(36535, vehicle.MSRP);
            Assert.AreEqual(true, vehicle.IsSold);
        }

        [Test]
        public void CanLoadVehicleById_Vehicle()
        {
            var repo = new VehicleRepository();
            var vehicle = repo.GetByIdByVehicle(4);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual(4, vehicle.VehicleId);
            Assert.AreEqual("inventory-4.jpg", vehicle.PhotoFile);
            Assert.AreEqual(2019, vehicle.Year);
            Assert.AreEqual(1, vehicle.MakeId);
            Assert.AreEqual(2, vehicle.ModelId);
            Assert.AreEqual(1, vehicle.BodyStyleId);
            Assert.AreEqual(false, vehicle.IsManualTransmission);
            Assert.AreEqual(4, vehicle.ExteriorColorId);
            Assert.AreEqual(2, vehicle.InteriorColorId);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual("123456789AAAAAAAD", vehicle.Vin);
            Assert.AreEqual("Sample data vehicle description", vehicle.Description);
            Assert.AreEqual(34535, vehicle.SalePrice);
            Assert.AreEqual(36535, vehicle.MSRP);
            Assert.AreEqual(true, vehicle.IsSold);
            Assert.AreEqual(true, vehicle.IsNew);
        }

        [Test]
        public void CanAddVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehicleRepository();

            vehicleToAdd.MakeId = 2;
            vehicleToAdd.ModelId = 5;
            vehicleToAdd.Year = 2015;
            vehicleToAdd.BodyStyleId = 1;
            vehicleToAdd.IsManualTransmission = false;
            vehicleToAdd.ExteriorColorId = 5;
            vehicleToAdd.InteriorColorId = 2;
            vehicleToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            vehicleToAdd.Mileage = 34000;
            vehicleToAdd.SalePrice = 27409;
            vehicleToAdd.MSRP = 28000;
            vehicleToAdd.Vin = "12121345678990909";
            vehicleToAdd.Description = "Test description";
            vehicleToAdd.PhotoFile = "";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsNew = false;
            vehicleToAdd.IsSold = false;

            repo.Add(vehicleToAdd);

            Assert.AreEqual(16, vehicleToAdd.VehicleId);
            Assert.AreEqual(2, vehicleToAdd.MakeId);
            Assert.AreEqual(5, vehicleToAdd.ModelId);
            Assert.AreEqual(2015, vehicleToAdd.Year);
            Assert.AreEqual(false, vehicleToAdd.IsManualTransmission);

        }

        [Test]
        public void CanEditVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehicleRepository();

            vehicleToAdd.MakeId = 2;
            vehicleToAdd.ModelId = 5;
            vehicleToAdd.Year = 2015;
            vehicleToAdd.BodyStyleId = 1;
            vehicleToAdd.IsManualTransmission = false;
            vehicleToAdd.ExteriorColorId = 5;
            vehicleToAdd.InteriorColorId = 2;
            vehicleToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            vehicleToAdd.Mileage = 34000;
            vehicleToAdd.SalePrice = 27409;
            vehicleToAdd.MSRP = 30000;
            vehicleToAdd.Vin = "12121345678990909";
            vehicleToAdd.Description = "Test description";
            vehicleToAdd.PhotoFile = "";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsNew = false;
            vehicleToAdd.IsSold = false;

            repo.Add(vehicleToAdd);

            vehicleToAdd.MakeId = 3;
            vehicleToAdd.ModelId = 4;
            vehicleToAdd.Year = 2016;
            vehicleToAdd.BodyStyleId = 2;
            vehicleToAdd.IsManualTransmission = false;
            vehicleToAdd.ExteriorColorId = 6;
            vehicleToAdd.InteriorColorId = 6;
            vehicleToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            vehicleToAdd.Mileage = 35000;
            vehicleToAdd.SalePrice = 32000;
            vehicleToAdd.Vin = "33121345678990909";
            vehicleToAdd.Description = "Test description";
            vehicleToAdd.PhotoFile = "";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsNew = false;
            vehicleToAdd.IsSold = false;

            repo.Edit(vehicleToAdd);

            var editedVehicle = repo.GetById(16);

            Assert.AreEqual(3, vehicleToAdd.MakeId);
            Assert.AreEqual(4, vehicleToAdd.ModelId);
            Assert.AreEqual(2016, vehicleToAdd.Year);
            Assert.AreEqual(2, vehicleToAdd.BodyStyleId);
            Assert.AreEqual(false, vehicleToAdd.IsManualTransmission);
            Assert.AreEqual(6, vehicleToAdd.ExteriorColorId);
            Assert.AreEqual(6, vehicleToAdd.InteriorColorId);
            Assert.AreEqual(35000, vehicleToAdd.Mileage);
            Assert.AreEqual(32000, vehicleToAdd.SalePrice);
            Assert.AreEqual("33121345678990909", vehicleToAdd.Vin);
            Assert.AreEqual("Test description", vehicleToAdd.Description);
            Assert.AreEqual(true, vehicleToAdd.IsFeatured);
            Assert.AreEqual(false, vehicleToAdd.IsNew);
            Assert.AreEqual(false, vehicleToAdd.IsSold);

        }


        [Test]
        public void CanSearchNewCarsOnMinYear()
        {
            var repo = new VehicleRepository();
            var found = repo.SearchNewInventory(new VehicleSearchParameters { MinYear = 2014 });

            Assert.AreEqual(7, found.Count());
        }

        [Test]
        public void CanSearchUsedCarsOnMinYear()
        {
            var repo = new VehicleRepository();
            var found = repo.SearchUsedInventory(new VehicleSearchParameters { MinYear = 2010 });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchNewCarsOnMaxPrice()
        {
            var repo = new VehicleRepository();
            var found = repo.SearchNewInventory(new VehicleSearchParameters { MaxPrice = 30000 });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchUsedCarsOnMaxPrice()
        {
            var repo = new VehicleRepository();
            var found = repo.SearchUsedInventory(new VehicleSearchParameters { MaxPrice = 20000 });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchNewCarsOnMinYearMaxPrice()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchNewInventory(new VehicleSearchParameters { MinYear = 2014, MaxPrice = 30000M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchUsedCarsOnMinYearMaxPrice()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchUsedInventory(new VehicleSearchParameters { MinYear = 2015, MaxPrice = 25000M });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchNewCarsOnMakeModelOrYear()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchNewInventory(new VehicleSearchParameters { MakeModelOrYear = "Out" });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchUsedCarsOnMakeModelOrYear()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchUsedInventory(new VehicleSearchParameters { MakeModelOrYear = "Hon" });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchNewCarsOnMakeAndMaxYear()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchNewInventory(new VehicleSearchParameters { MakeModelOrYear = "Asc", MaxYear = 2019 });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchNewCarsOnMakeAndMaxPrice()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchNewInventory(new VehicleSearchParameters { MakeModelOrYear = "Forest", MaxPrice = 30000 });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchAllCarsNotSoldNoParameters()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchAllNotSoldInventory(new VehicleSearchParameters { });

            Assert.AreEqual(11, found.Count());
        }

        [Test]
        public void CanSearchAllCarsNotSoldModelAndMinPrice()
        {
            var repo = new VehicleRepository();

            var found = repo.SearchAllNotSoldInventory(new VehicleSearchParameters { MakeModelOrYear = "Honda", MinPrice = 10000 });

            Assert.AreEqual(2, found.Count());
        }

        
        [Test]
        public void CanAddSale()
        {
            SaleItem saleToAdd = new SaleItem();
            var repo = new SalesRepository();

            saleToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            saleToAdd.Name = "TestName";
            saleToAdd.Phone = "333-999-9090";
            saleToAdd.Email = "test@yahoo.com";
            saleToAdd.Street1 = "street 1";
            saleToAdd.Street2 = "street 2";
            saleToAdd.City = "Depauw";
            saleToAdd.StateId = "IN";
            saleToAdd.ZipCode = 46777;
            saleToAdd.PurchaseTypeId = 2;
            saleToAdd.VehicleId = 10;
            saleToAdd.PurchasePrice = 30500;

            repo.Add(saleToAdd);

            Assert.AreEqual(5, saleToAdd.SalesId);
        }

        [Test]
        public void CanCountVehicles()
        {
            var repo = new VehicleRepository();
            var count = repo.CountOfVehicles();

            Assert.AreEqual(15, count);
        }

        [Test]
        public void CanGetMaxId()
        {
            var repo = new VehicleRepository();
            var maxId = repo.GetMaxVehicleId();

            Assert.AreEqual(15, maxId);
        }
    }
}
