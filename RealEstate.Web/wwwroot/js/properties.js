/* Client-side Search, Filtering & Sorting for Property Listing */
document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('search-keyword');
    const transactionTypeSelect = document.getElementById('filter-transaction-type');
    const propertyTypeSelect = document.getElementById('filter-property-type');
    const citySelect = document.getElementById('filter-city');
    const maxPriceInput = document.getElementById('filter-max-price');
    const bedroomsSelect = document.getElementById('filter-bedrooms');
    const sortSelect = document.getElementById('sort-properties');
    const resetBtn = document.getElementById('btn-reset-filters');

    const gridBtn = document.getElementById('btn-view-grid');
    const listBtn = document.getElementById('btn-view-list');
    const container = document.getElementById('properties-container');

    if (!container) return;

    // View mode switching
    if (gridBtn && listBtn) {
        gridBtn.addEventListener('click', function () {
            gridBtn.classList.add('active');
            listBtn.classList.remove('active');
            container.classList.remove('property-list-view');
        });

        listBtn.addEventListener('click', function () {
            listBtn.classList.add('active');
            gridBtn.classList.remove('active');
            container.classList.add('property-list-view');
        });
    }

    function applyFilters() {
        const keyword = searchInput?.value.toLowerCase().trim() || '';
        const transType = transactionTypeSelect?.value || '';
        const propType = propertyTypeSelect?.value || '';
        const city = citySelect?.value.toLowerCase().trim() || '';
        const maxPrice = parseFloat(maxPriceInput?.value) || 0;
        const beds = bedroomsSelect?.value || '';

        const cards = Array.from(document.querySelectorAll('.property-card-col'));
        let visibleCount = 0;

        cards.forEach(card => {
            const title = card.getAttribute('data-title')?.toLowerCase() || '';
            const cardCity = card.getAttribute('data-city')?.toLowerCase() || '';
            const price = parseFloat(card.getAttribute('data-price')) || 0;
            const cardTransType = card.getAttribute('data-transaction-type') || '';
            const cardPropType = card.getAttribute('data-property-type') || '';
            const cardBeds = card.getAttribute('data-bedrooms') || '';

            let matches = true;

            if (keyword && !title.includes(keyword) && !cardCity.includes(keyword)) matches = false;
            if (transType && cardTransType !== transType) matches = false;
            if (propType && cardPropType !== propType) matches = false;
            if (city && !cardCity.includes(city)) matches = false;
            if (maxPrice > 0 && price > maxPrice) matches = false;
            if (beds && cardBeds !== beds) matches = false;

            if (matches) {
                card.style.display = '';
                visibleCount++;
            } else {
                card.style.display = 'none';
            }
        });

        // Update count text
        const countText = document.getElementById('showing-count');
        if (countText) countText.textContent = visibleCount;

        const emptyState = document.getElementById('no-properties-found');
        if (emptyState) {
            emptyState.style.display = visibleCount === 0 ? 'block' : 'none';
        }

        // Apply Sorting
        sortCards();
    }

    function sortCards() {
        const sortVal = sortSelect?.value || 'newest';
        const cardsArr = Array.from(document.querySelectorAll('.property-card-col'));

        cardsArr.sort((a, b) => {
            const priceA = parseFloat(a.getAttribute('data-price')) || 0;
            const priceB = parseFloat(b.getAttribute('data-price')) || 0;
            const idA = parseInt(a.getAttribute('data-id')) || 0;
            const idB = parseInt(b.getAttribute('data-id')) || 0;

            if (sortVal === 'price-low') return priceA - priceB;
            if (sortVal === 'price-high') return priceB - priceA;
            return idB - idA; // default newest
        });

        cardsArr.forEach(card => container.appendChild(card));
    }

    // Event listeners
    [searchInput, transactionTypeSelect, propertyTypeSelect, citySelect, maxPriceInput, bedroomsSelect].forEach(input => {
        if (input) input.addEventListener('input', applyFilters);
    });

    if (sortSelect) sortSelect.addEventListener('change', sortCards);

    if (resetBtn) {
        resetBtn.addEventListener('click', function () {
            if (searchInput) searchInput.value = '';
            if (transactionTypeSelect) transactionTypeSelect.value = '';
            if (propertyTypeSelect) propertyTypeSelect.value = '';
            if (citySelect) citySelect.value = '';
            if (maxPriceInput) maxPriceInput.value = '';
            if (bedroomsSelect) bedroomsSelect.value = '';
            if (sortSelect) sortSelect.value = 'newest';
            applyFilters();
        });
    }

    // Initial filter check
    applyFilters();
});
