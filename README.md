# CallCenterProject
.NET Mvc Core 6'da, ORM Toollarından EntityFramework ile Codefirst tekniği kullanılarak geliştirdiğm CallCenter Projesidir


**CALL CENTER UYGULAMASI**

Müşteri Temsilcilerinin Müşterilerle görüşmelerini yönettiği bir uygulamadır.

Kullanıcı ; sisteme Üye Ol ekranından üye olduktan sonra Login olabilmektedir.Login olduğunda Müşteri mi Müşteri Temsilcisi mi ayrımı Role kontrolü ile sağlanır.

 - Eğer rolü customer ise Müşteri sayfasında  Taleplerini tarih sırasına göre görebilir ve Talep ekleyebilir.
 ![MüşteriTaleplerimSayfası](https://user-images.githubusercontent.com/104023688/233316733-4e2b339b-dafb-40b1-8bcd-fc65bf8f7d20.JPG)
 
 ![MüşteriTalepFormu](https://user-images.githubusercontent.com/104023688/233316818-a782a760-9ea0-4b1f-aeff-213d919bd78e.JPG)

 
 - Eğer rolü customerRep ise Müşteri Temsilcilerinin görebildiği “Müşteri Temsilcisi” butonu ile Müşteri Temsilcisi sayfasına geçilir.

 1.  Talepler sekmesinden;
Müşteri Taleplerini seçerse bütün Talepleri panelinde görebilir.
 2. Müşteri Talepleri sayfasından isteği Talebi alabilir.
 3. Talebi aldığında Talep Detay sayfasına geçer:
Talebin durumu “Talep değerlendiriliyor” olarak değişir ve hangi Müşteri Temsilcisinin ilgilendiği bilgisi ekranda gözükür.
 4. Talep Detay sayfasından “Talep Görüşme ” butonuna tıklayınca:
Müşteri Temsilcisi Görüşme Kayıtlarını form ekranından doldurup bilgileri kaydeder.
 5. Talep Görüşmesi kaydedildiğinde:
Talebin durumu “Talep çözüldü” olarak değişir ve “Kapanan Talepler” listesinde gözükür.


![1MüşteriTemsilcisi_MüşteriTalepleriSayfası](https://user-images.githubusercontent.com/104023688/233316882-bd15705f-3898-4183-916f-87d30b6f5767.JPG)

![2MüştTemsl_TalepDetayları](https://user-images.githubusercontent.com/104023688/233316900-6150b654-4f3f-48f1-935e-192a9bc977d9.JPG)

![3 GörüşmeFormu](https://user-images.githubusercontent.com/104023688/233316918-dd3592fb-2716-4a8d-9707-aa39ee6d83b0.JPG)

![4MüştTemsilcisiSayfaısnaDöner](https://user-images.githubusercontent.com/104023688/233316930-8c04ccf6-15e2-4583-9436-47787f7d2a69.png)

![5ÇözülenTalepler](https://user-images.githubusercontent.com/104023688/233316953-f07fc1a9-32f9-4bac-838a-784ff15bba87.JPG)


 - Database:
 
![database](https://user-images.githubusercontent.com/104023688/233322293-c7206154-7405-4b51-bf67-2a6f5a1a3cf6.jpg)


       **Refactoring 1** 
 Veritabanı bağlantısını dinamik hale getirildi.Veritabanı değişirse sadece ConnectionString'i değiştirmiş olucaz.
 CallCenterDbContext.cs classında OnConfiguring yazmak yerine appsettings.json içine ConnectionString yazıldı.
 ServiceExtentions.cs classında AddDbContext servisi eklendi.
 program.cs de bu ServiceExtentions class tanıtılıken parametre de eklenir
 CallCenterDbContext.cs clasına parametreli constructor eklendi

        **Refactoring 2**
 AddSingleton yerine AddScoped kullanıldı.
 Her istekte işlemler değiştiği, veritabanı güncelelndiği için bu yaklaşım doğru değil.Bu yüzden AddScoped kullanılır.
 AddSingleton : 1 kez oluşur Ram'e kaydeder ve hep onu kullanır.//Bütün requetlerde aynı instance'ı kullanır 
 AddScoped    : Her istekte 1 nesne üretir.//Gelen her aynı Requestte aynı instance'ı kullanır. 
                Farklı Requestlerde yeni bir instance oluşturup kullanır.
 AddTransient : Her kullanımda yeni nesne üretir.Aynı Requestte olsa yeni instance oluşturup kullanır

       **Refactoring 3**
 DataAccess kısmında her seferinde using içinde context (veritabanı nesnesi) üretmek yerine constructor üzerinden Dependency Injection ile nesneyi ürettiriz.LooseCouple ile bağımlılığını azaltmış oluruz.
 
        **Refactoring 4** 
 Generic tarafta EfEntityRepositoryBase.cs clasında  .SaveChanges() metotu oluşturuldu. Böylece Add,Update gibi işlemlerin içinde database e kaydetmek yerine bütün işlemler bittiğinde biz istediğimizde database e kaydetmiş oluruz.

       **Refactoring 5** 
 Müşteri Temsilcisi sayfasına;
   Dropboxın içine "Değerlendirilen Talepler" ve "Yeni Talepler" seçeneğide eklendi.
   Müşteri Temsilcisinin sadece kendi çözdüğü talepleri listeleyebilmesi için "Çözdüğüm Talepler" butonu eklendi.

       **Refactoring 6** 
 Admin -> Raporları görecek kişi ve MüşteriTemsilcilerini ayarlayan kişi
 Layoutu farklı oluşturuldu -> _AdminLayout.cshtml Yönetim Paneli sayfası yapıldı


