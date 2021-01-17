// Adam time for u to implement :blobsunglasses:
if(typeof(String.prototype.trim) === "undefined")
{
    String.prototype.trim = function() 
    {
        return String(this).replace(/^\s+|\s+$/g, '');
    };
}

// poopoohead

// Colour stuff
function getHue(prec) {
    return Math.floor(prec * 120);
}

function getClr(prec) {
    return [getHue(prec), 45, 75];
}

function HSL(a) {
    let [x, y, z] = a;
    return `hsl(${x}, ${y}%, ${z}%)`;
}

function setIconColor(clr) {
    $('footer .container div a i').css('color', clr);
    $('footer .container p a').css('color', clr);
}

function update(score, joke){
    let $body = $('body');
    let $moodInput = $('#mood-input');
    let $jokeOutput = $('#mood-joke');
    let $moodDiv = $('#mood-div');
    let $moodH = $('#mood-output');
    if ($moodInput.val().trim() === '') {
        $body.css('background-color', '#fbfbfb');
        setIconColor('#777777');
        $moodH.html('');
        $jokeOutput.html('');
        return;
    }
    let pcnt = Math.round(Math.abs(score) * 100);
    if(score === 0){
        $moodH.html(`Neutral :|`);
    }else if(score < 0){
        $moodH.html(pcnt + `% Negative :(`);
    }else{
        $moodH.html(pcnt + `% Positive :)`);
    }
    if(score <= -0.5){
        $jokeOutput.html(`Heres a random joke to cheer you up: ` + joke);
    }else{
        $jokeOutput.html('');
    }

    let prec = (score + 1) / 2, clr = getClr(prec); // advanced math
    $body.css('background-color', HSL(clr));
    clr[2] -= 50;
    setIconColor(HSL(clr));
}