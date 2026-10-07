✈️ Yeni bir case çalışmasını tamamladım: Booking.com API Entegrasyonlu Otel Arama Uygulaması!


🔧 Ne kurdum?

ASP.NET Core MVC üzerine katmanlı bir mimari (Controller → Service → Model) kurdum. API çağrılarını tek bir yerden, Dependency Injection ile yönetilen bir HttpClient servis katmanında topladım — Controller'lar sadece veriyi View'a taşıyor, API mantığına hiç karışmıyor.


🌍 API tarafı

RapidAPI üzerinden Booking.com API'sine bağlandım ve case'in istediği 3 adımlı akışı kurdum:

1️⃣ Location Search → kullanıcının yazdığı şehirden dest_id çekiliyor.

2️⃣ Hotel Search → o id ve arama kriterleriyle (tarih, kişi sayısı, para birimi) otel listesi geliyor.

3️⃣ Hotel Details → seçilen otelin detaylı bilgileri (açıklama, özellikler, fotoğraflar) ayrı sayfada gösteriliyor

🖥️ Dinamik render & hata yönetimi

Gelen JSON verisini Razor View'larda @foreach döngüleriyle sayfaya basıyorum — şehir bulunamazsa, API'den sonuç gelmezse ya da ağ hatası olursa kullanıcıya düzgün bir hata mesajı gösteriliyor; arama sırasında da tam ekran bir yükleniyor (loading state) animasyonu çalışıyor.


🔐 Güvenlik

RapidAPI key'ini appsettings.json'a değil, dotnet user-secrets ile yönettim — key hiçbir zaman repoya gitmedi.


🎨 Tasarım

Hazır temayı kullanmak yerine arayüzü sıfırdan tasarladım: seyahat temasına uygun "biniş kartı / pasaport damgası" motifiyle özgün bir görsel kimlik, scroll-spy navigasyon, animasyonlu puan rozetleri ve giriş animasyonları ekledim. ✨


🛠️Kullandığım teknolojiler:

🔸C#  

🔸ASP.NET Core MVC  

🔸Razor  

🔸RapidAPI (Booking.com API) 

🔸HttpClient & Dependency Injection 

🔸HTML/CSS/JS 


<img width="1593" height="575" alt="Ekran görüntüsü 2026-10-07 113756" src="https://github.com/user-attachments/assets/361981c2-40da-4906-9b1f-526f65df59c7" />
<img width="1878" height="1004" alt="Ekran görüntüsü 2026-10-07 113747" src="https://github.com/user-attachments/assets/bfb1077e-6f45-4a27-bd63-06c0be706a2b" />
<img width="1896" height="894" alt="Ekran görüntüsü 2026-10-07 113729" src="https://github.com/user-attachments/assets/30604705-bb2f-4d9d-8038-6f53a4918bff" />
<img width="1893" height="639" alt="Ekran görüntüsü 2026-10-07 113714" src="https://github.com/user-attachments/assets/134d7f66-3015-4da2-992d-06dff4ffe1a4" />
<img width="1892" height="750" alt="Ekran görüntüsü 2026-10-07 113659" src="https://github.com/user-attachments/assets/b9dc0dcc-e2ee-4395-a931-a10a84262db5" />
<img width="1890" height="1001" alt="Ekran görüntüsü 2026-10-07 113647" src="https://github.com/user-attachments/assets/fcd594ad-4807-4766-9623-1aa1560a4aa2" />
<img width="1639" height="561" alt="Ekran görüntüsü 2026-10-07 114150" src="https://github.com/user-attachments/assets/f2e57a74-7e40-45c7-96bb-1d061e7817cb" />
<img width="1866" height="906" alt="Ekran görüntüsü 2026-10-07 114146" src="https://github.com/user-attachments/assets/b16fd06c-0cc5-4e84-822c-f090c702558a" />
<img width="1117" height="701" alt="Ekran görüntüsü 2026-10-07 114136" src="https://github.com/user-attachments/assets/838733bf-f671-4603-9120-4b007cf5bdb7" />
<img width="1753" height="601" alt="Ekran görüntüsü 2026-10-07 114122" src="https://github.com/user-attachments/assets/9f4d0020-dfef-4bcc-a191-12aeb60cd8fd" />
<img width="1893" height="1022" alt="Ekran görüntüsü 2026-10-07 114051" src="https://github.com/user-attachments/assets/070e4d90-1a8b-4238-b23c-c8ad0fc55613" />
<img width="1331" height="952" alt="Ekran görüntüsü 2026-10-07 114008" src="https://github.com/user-attachments/assets/b9a177c5-4535-41eb-947e-c59fa5d0f4f2" />
<img width="1711" height="940" alt="Ekran görüntüsü 2026-10-07 113821" src="https://github.com/user-attachments/assets/bd296b9e-a0f0-476b-b17a-5c94e89a00c6" />
