import { useQuery } from "@tanstack/react-query";
import { getCafeByCafeId, getCafes, getCafesByLocation } from "./endpoints.cafe";

export const useGetCafesQuery = () => {
  return useQuery({
    queryKey: ["cafes"],
    queryFn: () => getCafes(),
  });
};

export const useGetCafeByCafeIdQuery = (cafeId, isEnabled) => {
  return useQuery({
    queryKey: ["cafe", { cafeId }],
    queryFn: () => getCafeByCafeId(cafeId),
    enabled: isEnabled,
  });
};

export const useGetCafesByLocationQuery = (locationFilter) => {
  return useQuery({
    queryKey: ["cafesByLocation", { locationFilter }],
    queryFn: () => getCafesByLocation(locationFilter),
  });
};
