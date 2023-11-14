VAR timeline = -1
VAR psikoloji = 0
Harum, parlak beyaz bir kapının önünde meşalelerin yandığı ama aydınlattığı bir yüzeyin olmadığı bir yerde dikilmiş duruyordu. #speaker: Narrator #portrait: Narrator
*[Devam]
"Neredeyim? Yoksa öldüm mü? Hayır, hayır hayır hayır... En büyük korkumun önünde dikiliyorum sadece." # speaker: Harum #portrait: Harum
    **[Devam]
    Üzerinde tüm birikmiş nefreti kusabileceği, travmalarının ve daha pek çok şeyin hesabını sorabileceği kardeşinin, kralının odasının önündeydi. Ona meydan okuduğu bir anı yaşıyordu.#speaker: Narrator #portrait: Narrator
        ***[Beyaz geçide doğru ilerle] -> In_The_Meeting_Room
        ***[Hayır! Biraz daha bekle]
        Beklemek her şeyi daha da kötü yapıyordu. Acı hissetmeyen vücudu kardeşinin eseri olan yara izlerine bakınca daha da kötü oluyordu. #speaker: Narrator #portrait: Narrator
            ****[Beyaz geçide doğru ilerle] -> In_The_Meeting_Room
            ****[(Beklemeye devam et...)]
            Vücudun titremeye başladı. Orada, geçidin önünde, güvenli bölgende beklemek daha iyi hissettiriyordu ama bunu yapman gerektiğini de biliyorsun.#speaker: Narrator #portrait: Narrator
            *****[Beyaz geçide doğru ilerle] -> In_The_Meeting_Room

=== In_The_Meeting_Room ===
    ~ timeline = timeline + 1
Kralı ve abisi Vallbergur kanlı canlı bir şekilde önünde dikiliyor. Geldiğini fark etmemiş gibi gözüküyor yoksa seni aşağılıyor mu acaba?#speaker: Narrator #portrait: Narrator
*[(Sessiz kal)]
* [(Dikkatini çekmek için mırıldan.)]
* [Buradayım, aynı buyurduğun gibi.]

    
-Hoşgeldin, canım kardeşim.#speaker: Vallbergur #portrait: Vallbergur
~ timeline = timeline + 1
*[(...)]
    Bugünkü cesaretin beni hayran bıraktı. Anlayacağın üzere seni de bunu takdir etmek için çağırdım.
    **[(...)]
    Kız kardeşimiz senin bu yaptığını görse gurur duyardı. Sanki başucunda söyleyeceklerini kulağına fısıldıyor gibi.
        ***["Rosie'nin adını ağzına alma!"]
        Ne diye bir anda tersledin? O benim de kız kardeşimdi. Canımdan çok severdim onu. Söylesene kız kardeşimi görüyor musun?
        -> AAA  
        ***[(Sessiz kal)]
        Söylesene Harum, onu görebiliyor musun?
        -> AAA


=== AAA ===
*[Evet, görebiliyorum.]
*[Görebiliyor olsaydım da bunu sana söylemezdim.]
- Demek ki görebiliyorsun
*[Konuşmayı bitir.] -> END