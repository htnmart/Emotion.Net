// Adam time for u to implement :blobsunglasses:
if(typeof(String.prototype.trim) === "undefined")
{
    String.prototype.trim = function() 
    {
        return String(this).replace(/^\s+|\s+$/g, '');
    };
}

function getScore(sentence) {
    const sc = sentence.split(' ').map(w => w.trim().replaceAll(/[.?!;\-,]/ig, ''))
        .filter(w => w == 'happy' || w == 'sad')
        .map(w => w == 'happy' ? 1 : -1)
        if (sc.length === 0) return 0;
    return sc.reduce((a, b) => a + b, 0) / sc.length;
}

function getEmotion(score) {
    if (score >= 0.5) return 'happy';
    else if (score <= -0.5) return 'sad';
    return 'i dunno';
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

$(function() {
    let $body = $('body');
    let $moodInput = $('#mood-input');
    let $moodDiv = $('#mood-div');
    let $moodH = $('#mood-output');

    console.log('orz sunflower');

    // listen for keypress :3
    $moodInput.keyup(function(evt) {
        if ($moodInput.val().trim() === '') {
            $body.css('background-color', '#fbfbfb');
            setIconColor('#777777');
            $moodH.html('');
            return;
        }
        
        let score = getScore($moodInput.val()), emotion = getEmotion(score);

        $moodH.html(`Mood: ${emotion}`);

        let prec = (score + 1) / 2, clr = getClr(prec); // advanced math
        $body.css('background-color', HSL(clr));
        clr[2] -= 50;
        setIconColor(HSL(clr));
    });
});