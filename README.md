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
 
![database](https://user-images.githubusercontent.com/104023688/233321558-d3e06cc1-535d-4caf-8583-162e3dbf3284.JPG)
