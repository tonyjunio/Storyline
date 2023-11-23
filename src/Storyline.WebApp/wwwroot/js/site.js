var bsMainOffCanvas = null;
let disableConsoleLog = true;

function ToggleMainOffCanvas() {

    if (bsMainOffCanvas == null) {
        var mainOffCanvas = document.getElementById('mainOffCanvas');
        bsMainOffCanvas = new bootstrap.Offcanvas(mainOffCanvas);
    }

    bsMainOffCanvas.toggle();
}

function RenderTimelineSummary() {
    let timelineSummary = document.getElementsByClassName("timeline-summary")[0];
    let periods = timelineSummary.querySelectorAll(".timeline-period");
    let events = timelineSummary.querySelectorAll(".event-item");
    let eventHeight = 24;
    let eventVerticalGap = 5;

    let tsWidth = timelineSummary.clientWidth;

    let timelinePoints = [];
    let distance = 0;
    periods.forEach((elem, idx) => {
        let elemWidth = elem.clientWidth;

        timelinePoints.push({
            year: elem.dataset.year,
            xIndex: idx,
            element: elem,
            width: elemWidth,
            startX: distance,
            endX: distance + elemWidth
        });

        distance += elemWidth;
    });
    ConsoleLog(timelinePoints);

    let eventPoints = [];
    let periodXyMap = [];
    events.forEach((elem) => {
        let eventTitle = elem.dataset.eventTitle;
        let startYear = elem.dataset.startYear;
        let endYear = elem.dataset.endYear;
        let startPoint = timelinePoints.find(point => point.year === startYear);
        let endPoint = timelinePoints.find(point => point.year === endYear);
        let eventWidth = endPoint.endX - startPoint.startX;

        let yIndex = 0;
        let yPosition = 0;

        if (periodXyMap.length > 0) {
            // If there is a (x, y) map information available, find the next available Y position that the event can be placed at.
            let isOccupied = true;
            let occupiedYPosition = 0;
            ConsoleLog("+++");
            while (isOccupied) {
                for (let x = startPoint.xIndex; x <= startPoint.xIndex; x++) {
                    let mapItem = periodXyMap.find(item => {
                        return item.xIndex === x && item.yIndex === yIndex;
                    });

                    ConsoleLog(mapItem);
                    if (mapItem != undefined) {
                        ConsoleLog(mapItem.yIndex);
                        occupiedYPosition = mapItem.yPosition + mapItem.height + eventVerticalGap;

                        // if  (x, y) map is found, increase yIndex and try again.
                        yIndex = Math.max(yIndex, mapItem.yIndex) + 1;
                        continue;
                    }

                    // Find the Y position where to place the next evet item.
                    yPosition = occupiedYPosition;

                    isOccupied = false;
                    break;
                }
            }
            ConsoleLog("+++");
        }

        ConsoleLog("--------------------------------------------------");
        ConsoleLog(`eventTitle: ${eventTitle}`);
        ConsoleLog(`startPoint.xIndex: ${startPoint.xIndex}`);
        ConsoleLog(`endPoint.xIndex: ${endPoint.xIndex}`);
        ConsoleLog(`yIndex: ${yIndex}`);
        ConsoleLog(`yPosition: ${yPosition}`);

        // Set the event styles to show on the timeline summary.
        elem.style.top = yPosition + 'px';
        elem.style.width = (eventWidth - 10) + 'px'; // minus 10 is to adjust for the 5px left position.
        elem.style.left = '5px';
        // elem.style.backgroundColor = RndRGBColorValue(colorElementMin = 95);

        let eventHeight = elem.clientHeight;

        eventPoints.push({
            eventTitle: eventTitle,
            startYear: startYear,
            endYear: endYear,
            startPointXIndex: startPoint.xIndex,
            endPointXIndex: endPoint.xIndex,
            startX: startPoint.startX,
            endX: endPoint.endX,
            width: eventWidth,
            height: elem.clientHeight,
            yIndex: yIndex,
            top: yPosition
        });

        // Add event (x,y) position.
        for (let x = startPoint.xIndex; x <= endPoint.xIndex; x++) {
            periodXyMap.push({
                xIndex: x,
                yIndex: yIndex,
                height: eventHeight,
                yPosition: yPosition,
                eventTitle: eventTitle
            });
        }
    });

    // Get all y values, then sort descendingly and finally get only the first value at first index.
    let positionHeights = periodXyMap.map((item) => item.yPosition + item.height).sort((a, b) => b - a)[0];

    // Set the timelineSummary height based on the event that occupied the highest Y position (+55px for the year header offset)
    timelineSummary.style.height = positionHeights + 55 + 'px';

    ConsoleLog(eventPoints);
    console.log("RenderTimelineSummary() completed");
}

function RndRGBColorValue(colorElementMin = 0, colorElementMax = 255) {
    let min = colorElementMin;
    let max = colorElementMax;
    let fn = () => Math.floor(Math.random(0) * (max - min + 1)) + min;
    let r = fn(); g = fn(); b = fn();
    return `rgba(${r}, ${g}, ${b}, 1)`
}

function ConsoleLog(msg) {
    if (!disableConsoleLog) {
        console.log(msg);
    }
}


window.onresize = () => RenderTimelineSummary();