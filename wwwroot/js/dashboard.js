window.initCardDrag = (dotNetRef) => {

    let activeCard = null;
    let ghost = null;
    let offsetX = 0;
    let offsetY = 0;

    const dayMap = {
        Sunday: 0,
        Monday: 1,
        Tuesday: 2,
        Wednesday: 3,
        Thursday: 4,
        Friday: 5,
        Saturday: 6
    };

    const getDropColumn = (x, y) => {
        const cols = document.querySelectorAll(".day-col");
        for (const col of cols) {
            const r = col.getBoundingClientRect();
            if (x >= r.left && x <= r.right && y >= r.top && y <= r.bottom) {
                return col;
            }
        }
        return null;
    };

    document.querySelectorAll(".card-days").forEach(card => {

        if (card.dataset.bound) return;
        card.dataset.bound = "true";

        card.style.touchAction = "none";

        card.addEventListener("pointerdown", e => {
            e.preventDefault();

            activeCard = card;

            const rect = card.getBoundingClientRect();
            offsetX = e.clientX - rect.left;
            offsetY = e.clientY - rect.top;

            ghost = card.cloneNode(true);
            ghost.classList.add("dragging");
            ghost.style.position = "fixed";
            ghost.style.left = rect.left + "px";
            ghost.style.top = rect.top + "px";
            ghost.style.width = rect.width + "px";
            ghost.style.height = rect.height + "px";
            ghost.style.pointerEvents = "none";
            ghost.style.zIndex = "9999";

            document.body.appendChild(ghost);

            card.setPointerCapture(e.pointerId);
        });

        card.addEventListener("pointermove", e => {
            if (!ghost) return;

            ghost.style.left = (e.clientX - offsetX) + "px";
            ghost.style.top = (e.clientY - offsetY) + "px";
        });

        card.addEventListener("pointerup", async e => {
            if (!activeCard) return;

            card.releasePointerCapture(e.pointerId);

            const dropCol = getDropColumn(e.clientX, e.clientY);

            ghost.remove();
            ghost = null;

            if (!dropCol) {
                activeCard = null;
                return;
            }

            const scheduleId = activeCard.dataset.scheduleId;
            if (!scheduleId) {
                activeCard = null;
                return;
            }

            const dayName = dropCol.dataset.day;
            const newDay = dayMap[dayName];
            if (newDay === undefined) {
                activeCard = null;
                return;
            }

            dropCol.appendChild(activeCard);

            try {
                await dotNetRef.invokeMethodAsync(
                    "UpdateWeekPlanningJs",
                    Number(scheduleId),
                    newDay
                );
            } catch (err) {
                console.error("Backend update failed", err);
            }

            activeCard = null;

            setTimeout(() => window.initCardDrag(dotNetRef), 0);
        });

        card.addEventListener("pointercancel", () => {
            if (ghost) ghost.remove();
            ghost = null;
            activeCard = null;
        });
    });
};
