/* Favorites management via localStorage */
const FAVORITES_KEY = 'realestate_favorites';

function getFavorites() {
    try {
        const favs = localStorage.getItem(FAVORITES_KEY);
        return favs ? JSON.parse(favs) : [];
    } catch (e) {
        console.error('Failed to parse favorites', e);
        return [];
    }
}

function isFavorite(propertyId) {
    const favs = getFavorites();
    return favs.includes(Number(propertyId));
}

function toggleFavorite(propertyId) {
    let favs = getFavorites();
    const idNum = Number(propertyId);
    const index = favs.indexOf(idNum);

    if (index > -1) {
        favs.splice(index, 1);
        showToast('Property removed from favorites', 'info');
    } else {
        favs.push(idNum);
        showToast('Property added to favorites ❤️', 'success');
    }

    try {
        localStorage.setItem(FAVORITES_KEY, JSON.stringify(favs));
    } catch (e) {
        console.error('Failed to save favorites', e);
    }

    updateFavoritesUI();
    return favs.includes(idNum);
}

function updateFavoritesUI() {
    const favs = getFavorites();
    
    // Update header badge
    const badge = document.getElementById('fav-count-badge');
    if (badge) {
        badge.textContent = favs.length;
        badge.style.display = favs.length > 0 ? 'inline-block' : 'none';
    }

    // Update favorite heart buttons
    document.querySelectorAll('.btn-fav').forEach(btn => {
        const id = btn.getAttribute('data-property-id');
        if (id) {
            if (isFavorite(id)) {
                btn.classList.add('active');
                btn.querySelector('i')?.classList.replace('bi-heart', 'bi-heart-fill');
            } else {
                btn.classList.remove('active');
                btn.querySelector('i')?.classList.replace('bi-heart-fill', 'bi-heart');
            }
        }
    });

    // If on favorites page, hide/show cards
    const favPageContainer = document.getElementById('favorites-cards-grid');
    if (favPageContainer) {
        let visibleCount = 0;
        document.querySelectorAll('.fav-property-card-wrapper').forEach(card => {
            const propId = Number(card.getAttribute('data-property-id'));
            if (favs.includes(propId)) {
                card.style.display = 'block';
                visibleCount++;
            } else {
                card.style.display = 'none';
            }
        });

        const emptyState = document.getElementById('no-favorites-empty');
        if (emptyState) {
            emptyState.style.display = visibleCount === 0 ? 'block' : 'none';
        }
    }
}

document.addEventListener('DOMContentLoaded', function () {
    updateFavoritesUI();

    document.addEventListener('click', function (e) {
        const favBtn = e.target.closest('.btn-fav');
        if (favBtn) {
            e.preventDefault();
            e.stopPropagation();
            const id = favBtn.getAttribute('data-property-id');
            if (id) {
                toggleFavorite(id);
            }
        }
    });
});
