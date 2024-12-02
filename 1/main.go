package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		fmt.Println("Error opening file", err)
		return
	}

	defer func(file *os.File) {
		err := file.Close()
		if err != nil {

		}
	}(file)

	// Create two vectors to store the values of each column
	firstColumn := make([]int, 1)
	secondColum := make([]int, 1)

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()

		parts := strings.Fields(line)
		if len(parts) != 2 {
			fmt.Println("Invalid input", line)
			continue
		}

		// Convert the string values to integers and store them in the vectors
		for i, part := range parts {
			num, err := strconv.Atoi(part)
			if err != nil {
				fmt.Println("Error converting in integer", part)
				continue
			}

			// Store the values in the corresponding column
			if i == 0 {
				firstColumn = append(firstColumn, num)
			} else {
				secondColum = append(secondColum, num)
			}
		}

		// Sort both columns
		sort.Ints(firstColumn)
		sort.Ints(secondColum)
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error reading file", err)
		return
	}

	sum := sumTotalDistance(firstColumn, secondColum)
	fmt.Printf("Total distance %d\n", sum)

	similarities := calculateSimilarities(firstColumn, secondColum)
	fmt.Printf("Similarities %d\n", similarities)
}

func sumTotalDistance(firstVector, secondVector []int) int {
	if len(firstVector) != len(secondVector) {
		fmt.Print("The vectors must have the same length")
		return 0
	}

	var sum int
	for i := 0; i < len(firstVector); i++ {
		diff := firstVector[i] - secondVector[i]
		sum += int((math.Abs(float64(diff))))
	}
	return sum
}

func calculateSimilarities(firstVector, secondVector []int) int64 {
	if len(firstVector) != len(secondVector) {
		fmt.Print("The vectors must have the same length")
		return 0
	}

	// Map
	var similarityValues = make(map[int]int)

	var similaritySum int64
	for _, fVectorValue := range firstVector {
		// Check if the value is already in the map
		if _, exists := similarityValues[fVectorValue]; !exists {
			similarityValues[fVectorValue] = 0
		}

		// Check if the value is in the second vector
		for _, sVectorValue := range secondVector {
			if sVectorValue == fVectorValue {
				similarityValues[fVectorValue]++
			}
		}
	}

	// Calculate the similarity sum looping through the map result in KeyValuePairs
	for key, value := range similarityValues {
		if value > 0 {
			similaritySum += int64(key * value)
		}
	}

	return similaritySum
}
