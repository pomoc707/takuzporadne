document.addEventListener("DOMContentLoaded", function () {
    const widgets = [
        // 1. Bible a tradice
        { title: "Bible a tradice", text: "Bible a tradice jdou ruku v ruce." }, // Pravoslavní (B1)
        { title: "Bible a tradice", text: "Bible je základ, ale i tradice jsou důležité pro lepší pochopení víry." }, // Katolíci (A1)
        { title: "Bible a tradice", text: "Bible je jediný pevný základ." }, // Protestanté (C1)
    
        // 2. Víra a životní styl
        { title: "Víra a život", text: "Modlitba, půst a konání dobra jsou klíčem k duchovnímu růstu." }, // Pravoslavní (B2)
        { title: "Víra a život", text: "Víra by se měla odrážet v každodenním životě." }, // Katolíci (A2)
        { title: "Víra a život", text: "Osobní vztah s Bohem je nejdůležitější ." }, // Protestanté (C2)
    
        // 3. Církevní vedení
        { title: "Kdo vede církev?", text: "Každá pravoslavná církev má svého patriarcu, ale nemá papeže." }, // Pravoslavní (B3)
        { title: "Kdo vede církev?", text: "Papež a biskupové pomáhají udržet jednotu a správný směr církve." }, // Katolíci (A3)
        { title: "Kdo vede církev?", text: "Žádná centrální autorita, každý sbor si řídí své věci samostatně." }, // Protestanté (C3)
    
        // 4. Svátosti
        { title: "Svátosti", text: "Svátosti jsou tajemství, skrze která Bůh působí v našem životě." }, // Pravoslavní (B4)
        { title: "Svátosti", text: "Sedm důležitých okamžiků na cestě víry, od křtu po svaté přijímání." }, // Katolíci (A4)
        { title: "Svátosti", text: "Uznávají jen dvě - křest a večeři Páně, protože jsou přímo v Bibli." }, // Protestanté (C4)
    
        // 5. Bohoslužby
        { title: "Bohoslužby", text: "Dlouhé, plné zpěvů a modliteb." }, // Pravoslavní (B5)
        { title: "Bohoslužby", text: "Slavnostní, plné symboliky, hudby a zpěvu." }, // Katolíci (A5)
        { title: "Bohoslužby", text: "Jednoduché, zaměřené na čtení Bible a kázání." }, // Protestanté (C5)
    
        // 7. Komunita a misie
        { title: "Komunita a cíl", text: "Rodina, modlitba a život ve společenství jsou klíčové." }, // Pravoslavní (B7)
        { title: "Komunita a cíl", text: "Víra je osobní, ale také se žije v církevním společenství." }, // Katolíci (A7)
        { title: "Komunita a cíl", text: "Každý křesťan by měl aktivně šířit evangelium." }, // Protestanté (C7)
    ];
    
        let currentIndex = 0;
    
        function updateWidgets() {
            const screenWidth = window.innerWidth;
            const widgetElements = document.querySelectorAll(".widget");
            const widgetNadpis = document.getElementById("widget-nadpis-title");
    
            if (!widgetNadpis || widgetElements.length === 0) {
                console.error("Nadpis nebo widgety nebyly nalezeny!");
                return;
            }
    
            if (screenWidth <= 800) {
                // SMALL SCREENS: Show 3 widgets, centered, with NO animation
                widgetElements.forEach((widget) => {
                    widget.style.transition = "none";
                    widget.style.opacity = "1"; // No fade effect
                    widget.style.textAlign = "center"; // Center text
                });
    
                widgetNadpis.innerText = widgets[currentIndex].title;
    
                widgetElements.forEach((widget, index) => {
                    let widgetIndex = (currentIndex + index) % widgets.length;
                    widget.style.display = "block";
                    widget.innerHTML = `<p>${widgets[widgetIndex].text}</p>`;
                });
    
            } else {
                // LARGE SCREENS: Keep animations with 3 widgets switching at a time
                widgetElements.forEach((widget, index) => {
                    let widgetIndex = (currentIndex + index) % widgets.length;
    
                    widget.classList.remove("enter", "exit");
    
                    // Apply exit animation
                    widget.classList.add("exit");
    
                    // Wait for animation, then update content
                    setTimeout(() => {
                        widget.innerHTML = `<p>${widgets[widgetIndex].text}</p>`;
                        widget.classList.remove("exit");
                        widget.classList.add("enter");
                    }, 300); // Match CSS animation time
                });
    
                widgetNadpis.innerText = widgets[currentIndex].title;
            }
        }
    
        // **Event listener for button click**
        document.getElementById("next-widget").addEventListener("click", function () {
            currentIndex = (currentIndex + 3) % widgets.length;
            updateWidgets();
        });
    
        // **Allow swipe on small screens**
        let touchStartX = 0;
        let touchEndX = 0;
    
        document.addEventListener("touchstart", function (event) {
            touchStartX = event.touches[0].clientX;
        });
    
        document.addEventListener("touchend", function (event) {
            touchEndX = event.changedTouches[0].clientX;
    
            if (touchEndX < touchStartX - 50) {
                // Swipe left → Next 3 widgets
                currentIndex = (currentIndex + 3) % widgets.length;
                updateWidgets();
            } else if (touchEndX > touchStartX + 50) {
                // Swipe right → Previous 3 widgets
                currentIndex = (currentIndex - 3 + widgets.length) % widgets.length;
                updateWidgets();
            }
        });
    
        // **Navigation toggle for mobile menu**
        const navToggle = document.getElementById("nav-toggle");
        const navMenu = document.getElementById("nav-menu");
    
        if (navToggle && navMenu) {
            navToggle.addEventListener("click", function () {
                navMenu.classList.toggle("open");
            });
        }
    
        // **Update on window resize**
        window.addEventListener("resize", updateWidgets);
    
        // **First update when page loads**
        updateWidgets();
    });


   
        document.addEventListener('DOMContentLoaded', function() {
            const logoElement = document.querySelector('#logo-image'); // Nebo document.querySelector('.logo');
            const defaultImage = 'alfaomega.svg'; // Cesta k výchozímu obrázku
            const hoverImage = 'alfaomegab.svg';
        
            if (logoElement) {
                logoElement.addEventListener('mouseover', function() {
                    logoElement.src = hoverImage;
                });
        
                logoElement.addEventListener('mouseout', function() {
                    logoElement.src = defaultImage;
                });
            } else {
                console.error('Element s ID "logo-image" nebyl nalezen.');
            }
        });
    