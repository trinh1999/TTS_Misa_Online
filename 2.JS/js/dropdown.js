// const selected = document.querySelector(".selected");
// const optionsContainer = document.querySelector(".options-container");

const selected = document.querySelectorAll(".selected");
const optionsContainer = document.querySelectorAll(".options-container");


selected.forEach(n => {
      n.addEventListener('click', () => {
        n.closest('.select-box').firstChild.nextSibling.classList.toggle('active');
      });
});

const optionsList = document.querySelectorAll(".option");
optionsList.forEach(n => {
  n.addEventListener("click", () => {
    let text = n.querySelector("label").innerHTML;
    n.closest('.options-container').classList.remove("active");
    n.closest('.options-container').nextElementSibling.innerHTML= text;
  });
});