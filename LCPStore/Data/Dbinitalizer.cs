using LCPStore.Controllers;
using LCPStore.Migrations;
using LCPStore.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace LCPStore.Data
{
    public class Dbinitalizer
    {
        private static object TotalPrice;
        private static object ordersItems;

        public static Order Order { get; private set; }
        public static object OrderItems { get; private set; }
        public static object OrdersItems { get; private set; }

        public static void Initialize(LCPStoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Category.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category{Name = "Outdoors and Camping"},
                new Category{Name = "Gadgets"},
                new Category{Name = "Baby Essentials"},
                new Category{Name = "Convenience"},
                new Category{Name = "Sports Accessories"}
            };


            foreach (Category c in categories)
            {
                context.Category.Add(c);
            }

            context.SaveChanges();
            var products = new Product[]
            {
                new Product{Name = "Portable Washing Machine and Dryer", Created = DateTime.Today,
                    Description = "An automatic washing machine that also functions as a clothes dryer" +
                    "Small, compact, lightweight, and very portable perfect for traveling, camping, vacations, and business trips " +
                    "Automatically shuts off after 30 minutes of work, so you can leave it so you can work on other chores " +
                    "Although small, it has an amazing power motor that thoroughly washes your clothes, underwear, and garments " +
                    "Energy - efficient, low power consumption " +
                    "Interface: USB / Input Voltage: DC 5V / Input Current: 2A / Rated Voltage: DC 10V / Rated Power: 18W " +
                    "Forward Reverse Time: 15 Seconds / Ultrasonic Working Time: 60 Seconds Switching " +
                    "Size: 17 x 17 x 17 cm / Housing and Tub Material: Plastic / Capacity: 2.1 - 4.5 kgs per load " +
                    "Package Content: Portable Washing Machine and Dryer",
                    Price = 75,Category=categories[0]},

                new Product{Name ="Umbrella Cap Hands-Free Umbrella Hat", Created = DateTime.Today,
                    Description = "Hands-free, lightweight, and durable, it is easy to store and carry" +
                    "You can wear it while working in the garden, at the beach, fishing, or to sporting events" +
                    "Protect yourself from rain, and sun, it has the adjustable size to get the perfect fit" +
                    "Materials: Silver Plasters and Steel; Number of ribs: 7 bones; Umbrella density: 170T; Net Weight: 120g/ 4.23oz" +
                    "For a list of sizes and measurements, please scroll down to the description area to find the sizing chart" +
                    "Package Includes:" +
                    "1 x Umbrella Cap Hands-Free Umbrella Hat",Price = 20,Category=categories[0] },

                new Product{Name ="Bluetooth Beanie Hat Headset Cap", Created = DateTime.Today,
                    Description = "Listen to your favorite music while wearing this Bluetooth beanie hat" +
                    "It has two high-quality built-in speakers and microphone as well so you can take calls with this" +
                    "It has echo cancellation and noise suppression, you can also wash this by easily taking out the speakers" +
                    "Battery: Lithium-ion Battery / Power: Micro USB / Transmission Distance: 10m" +
                    "Charging time: 2hrs / Bluetooth talk time: 3-6hrs / Sensitivity: 120±5dBdB / Resistance: 16ΩΩ" +
                    "Package Contents:" +
                    "1 x Bluetooth Beanie Hat Headset Cap" +
                    "1 x USB Charging Cable" , Price = 29,Category=categories[0] },

                new Product{Name ="Women’s Urinal Portable Funnel", Created = DateTime.Today,
                    Description = "This Women’s Urinal is lightweight and portable so it is perfect for traveling" +
                    "It is flexible and instantly takes its shape once it is taken out so it is easy to store" +
                    "Made of silicone material that is easily washable and reusable as well" +
                    "Material: TPR" +
                    "Size: 10 x 15 cm / Net Weight: 21g" +
                    "Package Contents:" +
                    "1 x Women’s Urinal", Price = 16,Category=categories[0] } ,
                new Product{Name ="Collapsible Bathtub Adult Size", Created = DateTime.Today,
                 Description = "An adult-size foldable bathtub with a headrest and side handles" +
                                "It has grooves on its base that prevent the user from slipping; Sturdy legs keep it upright and prevents wobbling" +
                                "It has a foot roller and massage grooves on the tail-end that you can use to massage your feet and legs" +
                                "Material: Plastic / Load Bearing: Max. 220 kgs" +
                                "Size Variants:" +
                                "Large: 138 x 62 x 52cm (With/Without Lid)" +
                                "Small: 120 x 62 x 52cm (With/Without Lid)" +
                                "Package Content:" +
                                "1 x Collapsible Bathtub",Price=815,Category=categories[0]},

                 new Product{Name ="Shower LED Light 7-Color Head", Created = DateTime.Today,
                 Description= "This Shower LED Light makes bathtimes even more special and relaxing" +
                                "It has seven light colors that automatically change regardless of water temperature" +
                                "It has a universal thread and runs on water pressure alone; no batteries needed" +
                                "Material: ABS Plastic / Showerhead Diameter: 78mm" +
                                "Length: 215mm / Thickness: 38mm" +
                                "Exterior Thread Size: 20mm (universal)" +
                                "Package Contents:" +
                                "1 x Shower LED Light", Price=29,Category=categories[1] },
                 new Product{Name ="Portable Picture Printer Device ", Created = DateTime.Today,
                             Description= "Print your favorite photos on your phone instantly using this portable picture printer" +
                                            "You can connect it to your phone wirelessly through Bluetooth and provides high-quality printing" +
                                            "This is portable and is about the size of a regular smartphone so it is easy to hold" +
                                            "Charging Time: 2.5 hrs / Battery Life: 23 photos / Weight: 195g / Battery: 500 mAh Li-ion Battery" +
                                            "Size: 120 x 80 x 22.3mm / Resolution: 313 x 490dpi / Print Speed: 55s / Photo Size: 2 x 3″ or 50 x 76mm" +
                                            "Tray Capacity: 10 sheets of photo paper + 1 blue card / Supported Operating Systems: Android 8.0 and iOS 10.0.2 or later, HUAWEI Share: EMUI 9.0.1 or later" +
                                            "Package Content per Bundle:" +
                                            "Bundle 1:" +
                                            "1 x Portable Picture Printer Device" +
                                            "1 x User Menu" +
                                            "Bundle 2:" +
                                            "1 x Portable Picture Printer Device" +
                                            "1 x User Menu" +
                                            "20 x Photo Paper", Price=125,Category=categories[1] },
                 new Product{Name ="Portable Small Bluetooth Keyboard ", Created = DateTime.Today,
                            Description="A wireless Bluetooth keyboard with a built-in Li-ion rechargeable battery" +
                                        "Slim, lightweight, portable, and with 49 alphanumeric keys" +
                                        "Bluetooth version: 3.0 / Charging Time: 3-4 hours" +
                                        "Dimensions: 114 x 60 x 9mm" +
                                        "Package content:" +
                                        "1 x Portable Small Bluetooth Keyboard" +
                                        "1 x Charging Cable" +
                                        "1 x user Manual", Price=42,Category=categories[1] },

                 new Product{Name ="Phone Sterilizer UV Light Sanitizing Box ", Created = DateTime.Today,
                 Description= "Keep your phone clean and free from germs and bacteria by using this phone sterilizer" +
                                "This sterilizer box uses UV light to effectively kill bacteria and it also has an aromatherapy function" +
                                "Can sterilize other items and is portable, plug it into the USB port on PC, power bank, or USB charger" +
                                "Capacity: Below 6.5 inches / Rated voltage: DC5V / Input current: 1-2A / Disinfection power: 5W" +
                                "Aromatherapy function: 1W / Power: 9W / UV wavelength: 253.7nm" +
                                "Product size: 218mm x 122mm x 23mm / Product weight: 300g" +
                                "Package Content:" +
                                "1 x Phone Sterilizer UV Light Sanitizing Box" +
                                "1 x USB Cable",Price=45,Category=categories[1] },
                 new Product{Name ="Baby Shoulder Carrier Saddle Seat", Created = DateTime.Today,
                             Description= "This baby shoulder carrier provides safe and convenient baby/kid carrying on your shoulders" +
                                            "It has a comfortable seat, a secure chest strap with a snap buckle, and two straps for the legs" +
                                            "This shoulder carrier is perfect for traveling, for visiting parks and zoos, and for everyday use" +
                                            "Maximum Carrying Capacity: 20 kgs / Suitable Age: 3 and Above" +
                                            "Adjustable Bust Size: 78 – 131 cm" +
                                            "Package Content:" +
                                            "1 x Baby Shoulder Carrier", Price=59,Category=categories[2] },

                    new Product{Name ="Baby Sun Tent Outdoor Sunshade", Created = DateTime.Today,
                                Description= "Protect your kids from the sun while still letting them enjoy the fine sunny beach, use this baby sun tent" +
                                                "This sun tent has its own swimming pool. So, your infant kay safely swim inside the comfort of their own tent" +
                                                "The tent is foldable, very lightweight, and portable. It also comes with a zipper closure storage pouch. It’s very easy to assemble too!" +
                                                "Capacity: 1-3 people (infant) / Size: 117 cm x 79 cm x 70 cm / Weight: 0.4-0.5kg" +
                                                "Storage Bag: Polyester Bag" +
                                                "Package Content:" +
                                                "1 x Baby Sun Tent" +
                                                "4 x Ground Nails", Price=40,Category=categories[2] },
                    new Product{Name ="Finger Baby Brush Silicone Toothbrush", Created = DateTime.Today,
                                Description= "This toothbrush is a silicone finger toothbrush with soft bristles. It’s gentle and very safe to use on your baby’s sensitive teeth and gums" +
                                            "Use it to remove milk residue, vegetable and fruit particles that get stuck on your baby’s little teeth" +
                                            "It comes with its own case so you can store it clean and dry after use" +
                                            "Material: Silicone / Net Weight: 13.5g" +
                                            "Container Size: 7 x 4 x 3cm (L x W x H) / Size: 5.5 x 2.3 cm (L x D)" +
                                            "Package Content:" +
                                            "1 x Finger Toothbrush + 1 x Toothbrush Container", Price=15,Category=categories[2] },
                    new Product{Name ="Baby Burrito Blanket Swaddle Wrap", Created = DateTime.Today,
                                Description= "The burrito blanket is a swaddle and blanket in a tortilla wrapper design" +
                                            "It has flour dough-like color and it even has burnt marks that make it look realistic" +
                                            "It’s a soft and velvety blanket, breathable too!" +
                                            "Material: 75% Polyester, 25% Cotton" +
                                            "Size: 85cm / 33.4 “/ Suitable for babies 0-3 months old" +
                                            "Package Content:" +
                                            "1 x Swaddle Blanket" +
                                            "1 x Hat", Price=28,Category=categories[2] },
                     new Product{Name ="Dog Trolley Pet Carrier Luggage", Created = DateTime.Today,
                                Description="A pet-friendly and ergonomically-designed trolley suitable for carrying cats and small breed dogs" +
                                            "The handle is foldable and expandable, it has an easy-carry grip, and universal wheels that turn 360 degrees" +
                                            "It has breathing holes, a bubble viewing window, a side pocket, and a side opening" +
                                            "Perfect for traveling, vet visits, and for everyday dog transport" +
                                            "Material: Oxford Fabric / Product Weight: ≈1.88kg / Product Size: 40 x 25 x 37 cm" +
                                            "Suitable For: Pets Below 14 kg" +
                                            "Package Content:" +
                                            "1 x Dog Trolley" +
                                            "1 x Bubble Window" +
                                            "1 x Fixed Ring" +
                                            "1 x Honeycomb Cover" +
                                            "1 x Washable Mat/Pad", Price=299,Category=categories[3] },

                     new Product{Name ="Sanitizer Bracelet Alcohol Dispenser Wristband", Created = DateTime.Today,
                             Description = "This sanitizer bracelet is a wristband that you can refill with alcohol or sanitizer" +
                                            "It offers convenient access for your sanitizer or alcohol. So, you can always keep your hands clean anytime and anywhere" +
                                            "This bracelet is refillable and it also comes with a squeeze bottle that you use to refill the tank when it’s running out of alcohol" +
                                            "It’s also suitable for sunscreen lotion, insect repellent, moisturizing lotion, and others" +
                                            "Material: Silicone / Capacity: 10 ml" +
                                            "Size: 68 x 63 x 20 mm / Weight: 9 g / Elastic, Adult Size, Unisex (Sanitizer NOT Included)" +
                                            "Package Contents:" +
                                            "1 x Sanitizer Bracelet + 1 x Squeeze Pump Bottle" , Price = 17,Category=categories[3] },

                     new Product{Name ="Penguin Egg Holder Boiling Tool", Created = DateTime.Today,
                                Description = "This penguin egg holder holds the eggs in place while it boils so they cook evenly and crack-free" +
                                                "This prevents the eggs from bouncing and bumping other eggs inside the pot" +
                                                "It has a cute and creative penguin design. When the egg is in the place, the penguin design becomes visible" +
                                                "It can hold six eggs at a time" +
                                                "Material: PP / Size: 14.6 x 12.1 x 12.4 cm" +
                                                "Package Content:" +
                                                "1 x Penguin Egg Holder (6 Slots)" , Price = 24,Category=categories[3] },

                      new Product{Name ="Cleaning Slippers Floor Mop Flip-Flops", Created = DateTime.Today,
                                  Description ="Clean your floor with every step and keep it shiny by wearing these cleaning slippers" +
                                                "These house slippers have these mop soles, they are also detachable so you can wash them" +
                                                "The slippers are soft and comfortable and covers your toes as well" +
                                                "Material: Coral Fleece + Chenille" +
                                                "Package Contents:" +
                                                "2 x Cleaning Slippers Floor Mop Flip-Flops (1 Pair)" , Price = 23,Category=categories[3] },

                      new Product{Name ="Men’s Gym Shorts with Inner Spandex Shorts", Created = DateTime.Today,
                                  Description = "A two-layered short composed of stretchable inner spandex and a quick-drying outer polyester short" +
                                                "It’s a comfortable fit short with a gartered waist, a drawstring waist rope, and a zippered pocket at the back" +
                                                "It has a towel holder slot that gives you easy access to your sweat towel while working out" +
                                                "These shorts complement the shape of your body that you worked hard for" +
                                                "Wide range of use: cycling, running, boxing, gym, basketball, etc" +
                                                "Inner Material: Spandex / Outer Material: Polyester / For Adult Men" +
                                                "For a list of sizes and measurements, please scroll down to the description area to find the sizing chart" +
                                                "Package Content:" +
                                                "1 x Men’s Gym Shorts" , Price = 32,Category=categories[4] },

                       new Product{Name ="Human Bubble Ball Inflatable Bump Ball", Created = DateTime.Today,
                                   Description ="The human bumper ball is an inflatable bumper ball that you wear on your upper body; It’s a fun outdoor game that you can play in pair and in teams" +
                                                "The ball has two smooth and secure grip handles with straps; It’s thick and durable and does not easily tear or burst" +
                                                "High-quality air vent prevents the air from leaking" +
                                                "Material: 0.8mm PVC / Size: 150 cm" +
                                                "Package Content:" +
                                                "1 x Bumper Ball", Price = 225,Category=categories[4] },

                        new Product{Name ="Portable Neck Fan Rechargeable Double Head Fan", Created = DateTime.Today,
                                    Description = "This is wearable and rechargeable neck fan with two fan heads" +
                                                    "It has a flexible frame so you can adjust the direction where the fans are facing, you can even rotate it to a 360-degree angle" +
                                                    "Three-speed levels and strong airflow; Long battery life" +
                                                    "Power Source: USB Cable / Charging Time: 2 Hours / Charging Input: DC 5V 1A" +
                                                    "Battery Life: 2-6 Hours / Consumption: 0.3-1.2W / Battery: 18650 Battery – 3.7V" +
                                                    "Size: 26 x 16cm / Weight: 180g" +
                                                    "Package Content:" +
                                                    "1x Portable Neck Fan" , Price = 36,Category=categories[4] },

                         new Product{Name ="Mist Spray Water Bottle Sports Bottle", Created = DateTime.Today,
                                     Description = "Use this mist spray water to keep you energized when you are playing sports or working out" +
                                                    "Has a spray function that you can use to splash some to your ace without wasting too much water" +
                                                    "Secure lock that will not spill any water and keep out dust and dirt" +
                                                    "Size: 28.5cm length x 5 cm diameter" +
                                                    "740ML" +
                                                    "Material: Tritan" +
                                                    "Package Content:" +
                                                    "1 x Mist Spray Water Bottle Sports Bottle" , Price = 29,Category=categories[4] } };
            foreach(Product p in products)
            {
                context.Product.Add(p);
            }

            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(1994,04,06),
                new DateTime(1996,08,11),
                new DateTime(1993,10,11),
                new DateTime(1995,09,14),
                new DateTime(2020,08,10),
                new DateTime(2020,10,1),
                new DateTime(2020,06,07),
                new DateTime(2020,08,11)
            };

            var accounts = new Account[]
            {
                new Account{Username="leelisker4@gmail.com",Password="1234",Gender=(Gender)1,Name="Likush",BirthDate=dateTimes[0], Registered = dateTimes[4], Role =(Role)0, Cart= new Cart() },
                new Account{Username="meitalzaguri95@gmail.com",Password="1234",Gender=(Gender)1,Name="Meital ",BirthDate=dateTimes[1], Registered = dateTimes[5], Role =(Role)0, Cart= new Cart() },
                new Account{Username="liraz981@gmail.com",Password="1234",Gender=(Gender)1,Name="Liraz Mizrachi",BirthDate=dateTimes[2], Registered = dateTimes[6], Role =(Role)1, Cart= new Cart() },
                new Account{Username="Mati340@gmail.com",Password="1234",Gender=(Gender)0,Name="Matan Katz",BirthDate=dateTimes[3], Registered = dateTimes[7], Role =(Role)0, Cart= new Cart() },
            };

            foreach (Account a in accounts)
            {
                context.Account.Add(a);
            }

        

            var orders = new Order[]
            {
                new Order{Country = "Israel", City = "Ashdod", Address = "Bareket 24", ZipCode = "1828373" ,PhoneNumber = "0521118765", TotalPay= 1630, Delivery = (Delivery)0, OrderTime = dateTimes[4] },
                new Order{Country = "Israel", City = "Yavne", Address = "Maapilim 16", ZipCode = "1874859" ,PhoneNumber = "052999362", TotalPay= 1705, Delivery = (Delivery)1, OrderTime = dateTimes[5]},
                new Order{Country = "Israel", City = "Eilat", Address = "Tamar 4", ZipCode = "7080000" ,PhoneNumber = "0546258943", TotalPay= 850, Delivery = (Delivery)0, OrderTime = dateTimes[6]},
                new Order{Country = "Israel", City = "Tzfat", Address = "Hashmonaim", ZipCode = "7546789" ,PhoneNumber = "0509876523", TotalPay= 460, Delivery = (Delivery)1, OrderTime = dateTimes[7]},

            };

            foreach (Order o in orders)

            {
                context.Order.Add(o);
            }
            OrderItem[] orderItems = new OrderItem[]
     {
                new OrderItem{ Quantity=2, Product=products[0], Order=orders[0], TotalPrice=3455 },
                new OrderItem{ Quantity=1, Product=products[1], Order=orders[0],TotalPrice=43 },
                new OrderItem{ Quantity=1, Product=products[7], Order=orders[1], TotalPrice=4545 },
                new OrderItem{ Quantity=3, Product=products[3], Order=orders[2], TotalPrice=47778 },
                new OrderItem{ Quantity=1, Product=products[9], Order=orders[2], TotalPrice=438 },
                new OrderItem{ Quantity=1, Product=products[14], Order=orders[3], TotalPrice=8676 }


     };
            foreach (OrderItem oi in orderItems)

            {
                context.OrderItem.Add(oi);
            }

            context.SaveChanges();

            }         




            }
                
        }

    

